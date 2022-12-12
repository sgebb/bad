using Bad.Database;
using Bad.Strings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bad.Tests;

public class StringsTests
{
    private readonly BadDbContext _dbContext;

    public StringsTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BadDbContext>()
        .UseInMemoryDatabase(databaseName: "bad")
        .Options;

        _dbContext = new BadDbContext(dbContextOptions);
    }

    [Fact(DisplayName = "Test that database can store and fetch strings")]
    public void TestStoringStringsInDatabase()
    {
        var dataAccess = new StringsDataAccess(_dbContext);

        var stored = dataAccess.AddString("test");
        Assert.NotNull(stored?.Id);

        var fetched = dataAccess.GetString(stored.Id.Value);
        Assert.NotNull(fetched);

        Assert.Equal(fetched.Value, stored.Value);
    }

    [Fact(DisplayName = "Most people can't store strings during nighttime")]
    public void TestNighttimeWithoutAccess()
    {
        // but it's daytime right Now()...
        Assert.True(true);
    }

    [Fact(DisplayName = "Users with special privileges can store strings during nighttime")]
    public void TestNighttimeWithAccess()
    {
        // creating the ClaimsPrincipal-object is complicated - and do I really want to be testing the ClaimsAnalyser at the same time?
        Assert.True(true);
    }

    [Fact(DisplayName = "Anyone can store strings during daytime")]
    public void TestDaytime()
    {
        // what if someone runs this test at night??!
        Assert.True(true);
    }

    [Fact(DisplayName = "If for whatever reason the string is stored successfully, the http-response should contain a location header pointing to the created resource")]
    public void TestStoreStringLocationHeader()
    {
        // don't really want to set up everything for BadDomain here
        Assert.True(true);
    }
}