namespace Bad.tests
{
    public class StringsDomainTests
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

        [Test(Description = "Verify that you can't do store a string between 24.00 and 07.00")]
        public void TestOfTime()
        {
            Assert.Pass();
        }

        [Test(Description = "Check that you can store a string at night if you have the right access")]
        public void TestOfTimeWithAccess()
        {
            Assert.Pass();
        }
    }
}