namespace Bad.tests
{
    public class NumbersControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //1: må kunne sende inn egen dbcontext
        //2: har lyst tli å teste controller-koden uten at databasen brukes

        [Test(Description = "Test something in the controller-layer")]
        public void TestSomeControllerSpecificThing()
        {
            Assert.Pass();
        }

        [Test(Description = "Test that you can add and remove and query from database (but does this test mean that the database is used correctly)")]
        public void TestDatabase()
        {
            Assert.Pass();
        }

        [Test(Description = "Test that the number stored in StoreNumberBetween")]
        public void TestRandomFunction()
        {
            Assert.Pass();
        }
    }
}