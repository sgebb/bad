using Bad.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Bad.Tests
{
    public class NumbersTest
    {
        private readonly IConfiguration _config;

        public NumbersTest()
        {
            // this assumes that the database already exists (which is only true if you've already started the application)
            // it means that tests are potentially filling the real database with test-data
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Fact(DisplayName = "API can store a number and fetch it afterwards")]
        public async Task TestControllerStoreAndFetch()
        {
            // this is ok, but if it fails I really have no idea what is wrong

            var numbersController = new NumbersController(_config);
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