namespace Bad.tests
{
    public class StringsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test(Description = "Verify that BadDomain-businesslogic does something (should probably mock dependencies?)")]
        public void TestOnlyBusinessLogic()
        {
            Assert.Pass();
        }

        [Test(Description = "Most people can't store strings during nighttime")]
        public void TestNighttimeWithoutAccess()
        {
            // but it's daytime?
            Assert.Pass();
        }

        [Test(Description = "Users with special privileges can store strings during nighttime")]
        public void TestNighttimeWithAccess()
        {
            // creating the ClaimsPrincipal-object is exhausting - and do I really want to be testing the ClaimsAnalyser at the same time?
            Assert.Pass();
        }

        [Test(Description = "Anyone can store strings during daytime")]
        public void TestDaytime()
        {
            // what if someone runs this test at night??!
            Assert.Pass();
        }

        [Test(Description = "If for whatever reason the string is stored successfully, the http-response should contain a location header pointing to the created resource")]
        public void TestStoreStringLocationHeader()
        {
            // don't really want to test logic in BadDomain here...
            Assert.Pass();
        }
    }
}