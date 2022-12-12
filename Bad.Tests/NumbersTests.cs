using Bad.Database;
using Bad.Numbers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bad.Tests;

public class NumbersTests
{
    private readonly BadDbContext _badDbContext;

    public NumbersTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BadDbContext>()
        .UseInMemoryDatabase(databaseName: "bad")
        .Options;

        _badDbContext = new BadDbContext(dbContextOptions);
    }

    [Fact(DisplayName = "API can store a number and fetch it afterwards")]
    public void TestControllerStoreAndFetch()
    {
        var desiredNumber = 10;
        var desiredEntity = new NumberEntity(desiredNumber){ Id = 1 };
        var fakeDataAccess = A.Fake<INumbersDataAccess>();

        // this mock is a bit artificial, but the point is that I am now testing controller-code
        A.CallTo(() => fakeDataAccess.StoreNumber(A<int>._)).Returns(desiredEntity);
        A.CallTo(() => fakeDataAccess.GetNumber(A<int>._)).Returns(desiredEntity);

        var numbersController = new NumbersController(fakeDataAccess);

        var response = numbersController.PostNumber(desiredNumber);
        var postedNumber = GetObjectResultContent(response);
        Assert.NotNull(postedNumber.Id);

        var fetched = numbersController.GetNumber(postedNumber.Id.Value);
        var fetchedNumber = GetObjectResultContent(fetched);

        Assert.Equal(fetchedNumber.Value, postedNumber.Value);
    }

    [Fact(DisplayName = "Adding and retrieving number works in the database (not the controller)")]
    public void TestDatabaseLayer()
    {
        var numbersDataAccess = new NumbersDataAccess(_badDbContext);
        var added = numbersDataAccess.StoreNumber(10);
        var fetched = numbersDataAccess.GetNumber(added.Id!.Value);

        Assert.NotNull(fetched);
        Assert.Equal(added.Value, fetched.Value);
    }

    // helps make sense of ActionResult-objects returned from aspnet-controllers
    private static T GetObjectResultContent<T>(ActionResult<T> result)
    {
        return (T)((ObjectResult)result.Result).Value;
    }
}