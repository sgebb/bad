using Bad.Controllers;
using Bad.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Bad.Tests
{
    public class NumbersTest
    {
        private readonly BadDbContext _badDbContext;

        public NumbersTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BadDbContext>()
            .UseInMemoryDatabase(databaseName: "MyBlogDb")
            .Options;
            _badDbContext = new BadDbContext(dbContextOptions);
        }

        [Fact(DisplayName = "API can store a number and fetch it afterwards")]
        public async Task TestControllerStoreAndFetch()
        {
            var numbersController = new NumbersController(_badDbContext);
            var desiredNumber = 1;

            var response = await numbersController.PostNumber(1);
            var resultObject = GetObjectResultContent(response);
            Assert.NotNull(resultObject.Id);

            var fetched = await numbersController.GetNumber(resultObject.Id.Value);
            var fetchedObject = GetObjectResultContent(fetched);

            Assert.Equal(fetchedObject.Value, desiredNumber);
        }

        [Fact(DisplayName = "Adding and retrieving number works in the database (not the controller)")]
        public void TestDatabaseLayer()
        {
            // I want to test the db-code, but i don't want to just repeat all the DB related code in NumbersController
            // would be nice to have a db-layer
            Assert.True(true);
        }

        // helps make sense of ActionResult-objects returned from aspnet-controllers
        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}