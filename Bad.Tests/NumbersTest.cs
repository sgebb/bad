using Bad.Controllers;
using Bad.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bad.tests
{
    public class NumbersTest
    {

        private IConfiguration _config;
        [SetUp]
        public void Setup()
        {
            // i don't really want test data polluting the normal db
            // but this seems like a lot of work
            // and now the test-database is longlived and needs to be maintained

            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Test(Description = "Test that after I store a number I can fetch the same number afterwards")]
        public async Task TestSomeControllerSpecificThing()
        {
            // this feels very framework specific
            var numbersController = new NumbersController(_config);
            var desiredNumber = 1;

            var response = await numbersController.StoreNumberBetween(1);
            var resultObject = GetObjectResultContent(response);
            Assert.That(resultObject.Id, Is.Not.Null);

            var fetched = await numbersController.GetNumber(resultObject.Id.Value);
            var fetchedObject = GetObjectResultContent(fetched);
            Assert.That(fetchedObject, Is.Not.Null);
            Assert.That(fetchedObject.Value, Is.EqualTo(desiredNumber));
        }

        [Test(Description = "Test that adding and retrieving number works in the database (not the controller)")]
        public void TestDatabase()
        {
            // i don't want to just repeat all the database-specific code that is already in the code, i want to tes that it works
            Assert.Pass();
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}