using AngleSharp.Dom;
using Blazored.LocalStorage;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared;

namespace BlazorApp.Tests.Acceptance;

public class CookiesComplianceAcceptanceTest : IAsyncDisposable
{
    private const string CookiesAcceptedDate = "CookiesAcceptedDate";
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

        var popupElement = app.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]", "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
        
        var acceptedLocalStorageEntry = await GetAcceptedLocalStorageEntry();
        Assert.Null(acceptedLocalStorageEntry);
 
        AssertPopupIsShown(popupElement);

        var approveButton = app.FindElementByCssSelectorAndTextContent("button", "Accept");
        approveButton!.Click();

        acceptedLocalStorageEntry = await _mockLocalStorage.GetItemAsync<long?>(CookiesAcceptedDate);
        Assert.NotNull(acceptedLocalStorageEntry);
        
        popupElement = app.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]", "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
        AssertPopupIsNotShown(popupElement);
    }

    [Fact]
    public async Task ShouldLetTheUserAcceptCookiesIfTheyHaveBeenApprovedMoreThanSixMonthsAgo()
    {
        var sevenMonthsAgo = DateTimeOffset.Now.AddMonths(-7).ToUnixTimeMilliseconds();
        await _mockLocalStorage.SetItemAsync(CookiesAcceptedDate, sevenMonthsAgo);
        
        var app = _testCtx.RenderComponent<CookiesPopup>();

        var popupElement = app.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]", "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
 
        AssertPopupIsShown(popupElement);

        var approveButton = app.FindElementByCssSelectorAndTextContent("button", "Accept");
        approveButton!.Click();

        var acceptedLocalStorageEntry = await _mockLocalStorage.GetItemAsync<long?>(CookiesAcceptedDate);
        Assert.NotNull(acceptedLocalStorageEntry);
        Assert.NotEqual(sevenMonthsAgo, acceptedLocalStorageEntry);
        
        popupElement = app.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]", "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
        AssertPopupIsNotShown(popupElement);
    }

    private static void AssertPopupIsShown(IElement? cookiesPopup)
    {
        Assert.Contains(cookiesPopup!.ClassList, c => c == "fixed");
    }
    
    private static void AssertPopupIsNotShown(IElement? cookiesPopup)
    {
        Assert.Contains(cookiesPopup!.ClassList, c => c == "hidden");
    }

    private ValueTask<long?> GetAcceptedLocalStorageEntry()
    {
        return _mockLocalStorage.GetItemAsync<long?>(CookiesAcceptedDate);
    }

    public async ValueTask DisposeAsync()
    {
        await _mockLocalStorage.ClearAsync();
    }
}