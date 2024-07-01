using Blazored.LocalStorage;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared;

namespace BlazorApp.Tests.Acceptance;

public class CookiesComplianceAcceptanceTest
{
    private readonly TestContext _testCtx;
    private ILocalStorageService _mockLocalStorage;

    public CookiesComplianceAcceptanceTest()
    {
        _testCtx = new TestContext();
        _testCtx.Services.AddScoped<ICookiesService, CookiesService>();
        _mockLocalStorage = _testCtx.AddBlazoredLocalStorage();
    }

    [Fact]
    public async Task ShouldLetTheUserAcceptCookiesIfTheyHaveNotBeenApprovedYet()
    {
        var app = _testCtx.RenderComponent<CookiesPopup>();
        

        var cookiesPopup = app.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]", "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
        
        var acceptedLocalStorageEntry = await GetAcceptedLocalStorageEntry();
        Assert.Null(acceptedLocalStorageEntry);
 
        Assert.NotNull(cookiesPopup);
        
        
        var approveButton = cookiesPopup.QuerySelector("button");
        approveButton!.Click();

        acceptedLocalStorageEntry = await _mockLocalStorage.GetItemAsync<String>("CookiesAcceptedDate");
        Assert.NotNull(acceptedLocalStorageEntry);
    }

    private ValueTask<string?> GetAcceptedLocalStorageEntry()
    {
        return _mockLocalStorage.GetItemAsync<String>("CookiesAcceptedDate");
    }

    // Not accepted yet
    // Accepted more than 6 months ago
}