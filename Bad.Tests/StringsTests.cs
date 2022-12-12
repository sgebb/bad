using Bad.Database;
using Bad.Strings;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Xunit;

namespace Bad.Tests;

public class StringsTests
{

    [Fact(DisplayName = "Test that database can store and fetch strings")]
    public void TestStoringStringsInDatabase()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BadDbContext>()
        .UseInMemoryDatabase(databaseName: "bad")
        .Options;

        var dbContext = new BadDbContext(dbContextOptions);

        var dataAccess = new StringsDataAccess(dbContext);

        var stored = dataAccess.AddString("test");
        Assert.NotNull(stored?.Id);

        var fetched = dataAccess.GetString(stored.Id.Value);
        Assert.NotNull(fetched);

        Assert.Equal(fetched.Value, stored.Value);
    }

    [Fact(DisplayName = "Most people can't store strings during nighttime")]
    public void TestNighttimeWithoutAccess()
    {
        var nightTimeProvider = A.Fake<ITimeProvider>();
        A.CallTo(() => nightTimeProvider.Now()).Returns(new DateTimeOffset(2022, 10, 10, 03, 10, 10, TimeSpan.Zero));

        var noAccessClaimsAnalyzer = A.Fake<IClaimsAnalyzer>();
        A.CallTo(() => noAccessClaimsAnalyzer.HasNightPrivileges(A<ClaimsPrincipal>._)).Returns(false);

        var stringToStore = Guid.NewGuid().ToString();

        var makeObjectDbAccess = A.Fake<IStringsDataAccess>();
        A.CallTo(() => makeObjectDbAccess.AddString(A<string>._)).Returns(new StringEntity(stringToStore));

        var domain = new StringsDomain(makeObjectDbAccess, nightTimeProvider, noAccessClaimsAnalyzer);
        var respons = domain.AddString(stringToStore, new ClaimsPrincipal());

        Assert.Null(respons); //meaning string was not stored...
    }

    [Fact(DisplayName = "Users with special privileges can store strings during nighttime")]
    public void TestNighttimeWithAccess()
    {
        var nightTimeProvider = A.Fake<ITimeProvider>();
        A.CallTo(() => nightTimeProvider.Now()).Returns(new DateTimeOffset(2022, 10, 10, 03, 10, 10, TimeSpan.Zero));

        var hasAccessClaimsAnalyzer = A.Fake<IClaimsAnalyzer>();
        A.CallTo(() => hasAccessClaimsAnalyzer.HasNightPrivileges(A<ClaimsPrincipal>._)).Returns(true);

        var stringToStore = Guid.NewGuid().ToString();

        var makeObjectDbAccess = A.Fake<IStringsDataAccess>();
        A.CallTo(() => makeObjectDbAccess.AddString(A<string>._)).Returns(new StringEntity(stringToStore));

        var domain = new StringsDomain(makeObjectDbAccess, nightTimeProvider, hasAccessClaimsAnalyzer);
        var respons = domain.AddString(stringToStore, new ClaimsPrincipal());

        Assert.NotNull(respons);
        Assert.Equal(stringToStore, respons.Value);
    }

    [Fact(DisplayName = "Anyone can store strings during daytime")]
    public void TestDaytime()
    {
        var daytimeProvider = A.Fake<ITimeProvider>();
        A.CallTo(() => daytimeProvider.Now()).Returns(new DateTimeOffset(2022, 10, 10, 14, 10, 10, TimeSpan.Zero));

        var noClaimsAnalyzer = A.Fake<IClaimsAnalyzer>();
        A.CallTo(() => noClaimsAnalyzer.HasNightPrivileges(A<ClaimsPrincipal>._)).Returns(false);

        var stringToStore = Guid.NewGuid().ToString();

        var makeObjectDbAccess = A.Fake<IStringsDataAccess>();
        A.CallTo(() => makeObjectDbAccess.AddString(A<string>._)).Returns(new StringEntity(stringToStore));

        var domain = new StringsDomain(makeObjectDbAccess, daytimeProvider, noClaimsAnalyzer);
        var respons = domain.AddString(stringToStore, new ClaimsPrincipal());

        Assert.NotNull(respons);
        Assert.Equal(stringToStore, respons.Value);
    }

    [Fact(DisplayName = "If for whatever reason the string is stored successfully, the http-response should contain a location header pointing to the created resource")]
    public void TestStoreStringLocationHeader()
    {
        var stringToStore = Guid.NewGuid().ToString();

        var fakeDomain = A.Fake<IStringsDomain>();
        A.CallTo(() => fakeDomain.AddString(A<string>._, A<ClaimsPrincipal>._)).Returns(new StringEntity(stringToStore) { Id = 1 });
        var controller = new StringsController(fakeDomain);

        var response = controller.StoreString(stringToStore);

        var createdResult = Assert.IsType<CreatedResult>(response.Result);
        Assert.Equal("/1", createdResult.Location);
        Assert.Equal(201, createdResult.StatusCode);
    }
}