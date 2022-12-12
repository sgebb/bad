using Bad.Database;
using Bad.Numbers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bad.Tests
{
    public class NumbersTest
    {
        private readonly BadDbContext _badDbContext;
        private readonly INumbersDataAccess _numbersDataAccess;

        public NumbersTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BadDbContext>()
            .UseInMemoryDatabase(databaseName: "bad")
            .Options;
            _badDbContext = new BadDbContext(dbContextOptions);

            _numbersDataAccess = new NumbersDataAccess(_badDbContext);

        }

        [Fact(DisplayName = "API can store a number and fetch it afterwards")]
        public void TestControllerStoreAndFetch()
        {
            var numbersController = new NumbersController(_numbersDataAccess);
            var desiredNumber = 1;

            var response = numbersController.PostNumber(1);
            var resultObject = GetObjectResultContent(response);
            Assert.NotNull(resultObject.Id);

            var fetched = numbersController.GetNumber(resultObject.Id.Value);
            var fetchedObject = GetObjectResultContent(fetched);

            Assert.Equal(fetchedObject.Value, desiredNumber);
        }

        [Fact(DisplayName = "Adding and retrieving number works in the database (not the controller)")]
        public void TestDatabaseLayer()
        {
            var added = _numbersDataAccess.StoreNumber(10);
            var fetched = _numbersDataAccess.GetNumber(added.Id!.Value);

            Assert.NotNull(fetched);
            Assert.Equal(added.Value, fetched.Value);
        }

        // helps make sense of ActionResult-objects returned from aspnet-controllers
        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}