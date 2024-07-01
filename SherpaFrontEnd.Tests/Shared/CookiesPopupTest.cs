using AngleSharp.Dom;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared;

namespace BlazorApp.Tests.Shared;

public class CookiesPopupTest
{
    private readonly TestContext _testContext;
    private readonly Mock<ICookiesService> _mockCookiesService;
    private const string FunctionName = "startCollectingAnalyticsData";

    public CookiesPopupTest()
    {
        _testContext = new TestContext();
        _mockCookiesService = new Mock<ICookiesService>();
        _testContext.JSInterop.SetupVoid(FunctionName).SetVoidResult();
    }

    [Fact]
    public void ShouldNotShowIfCookiesAreAlreadyAccepted()
    {
        _mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(true);

        _testContext.Services.AddScoped<ICookiesService>(sp => _mockCookiesService.Object);

        var cut = _testContext.RenderComponent<CookiesPopup>();

        var cookiesPopup = cut.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]",
            "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");

        _mockCookiesService.Verify(mock => mock.AreCookiesAccepted(), Times.Once);

        AssertPopupIsNotShown(cookiesPopup);
    }

    [Fact]
    public void ShouldShowIfCookiesAreNotAccepted()
    {
        _mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(false);

        _testContext.Services.AddScoped<ICookiesService>(sp => _mockCookiesService.Object);

        var cut = _testContext.RenderComponent<CookiesPopup>();

        var cookiesPopup = cut.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]",
            "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");

        _mockCookiesService.Verify(mock => mock.AreCookiesAccepted(), Times.Once);

        AssertPopupIsShown(cookiesPopup);
    }

    [Fact]
    public void ShouldSetCookiesAcceptedWhenButtonIsClicked()
    {
        _mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(false);

        _testContext.Services.AddScoped<ICookiesService>(sp => _mockCookiesService.Object);

        var cut = _testContext.RenderComponent<CookiesPopup>();

        var button = cut.FindElementByCssSelectorAndTextContent("button", "Accept");

        button!.Click();

        cut.WaitForAssertion(() => { _mockCookiesService.Verify(mock => mock.AcceptCookies(), Times.Once); });
    }

    [Fact]
    public void ShouldHideAfterAcceptingCookies()
    {
        _mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(false);

        _testContext.Services.AddScoped<ICookiesService>(sp => _mockCookiesService.Object);

        var cut = _testContext.RenderComponent<CookiesPopup>();

        var button = cut.FindElementByCssSelectorAndTextContent("button", "Accept");

        button!.Click();

        cut.WaitForAssertion(() =>
        {
            var cookiesPopup = cut.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]",
                "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
            AssertPopupIsNotShown(cookiesPopup);
        });
    }

    [Fact]
    public void ShouldStartCollectingAnalyticsAfterCookiesAreAccepted()
    {
        _mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(false);

        _testContext.Services.AddScoped<ICookiesService>(sp => _mockCookiesService.Object);

        var cut = _testContext.RenderComponent<CookiesPopup>();

        var button = cut.FindElementByCssSelectorAndTextContent("button", "Accept");

        button!.Click();
        
        cut.WaitForAssertion(() =>
        {
            _testContext.JSInterop.VerifyInvoke(FunctionName);
        });
    }
    
    [Fact]
    public void ShouldStartCollectingAnalyticsIfCookiesAreAccepted()
    {
        _mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(true);

        _testContext.Services.AddScoped<ICookiesService>(sp => _mockCookiesService.Object);

        var cut = _testContext.RenderComponent<CookiesPopup>();
        
        cut.WaitForAssertion(() =>
        {
            _testContext.JSInterop.VerifyInvoke(FunctionName);
        });
    }

    private static void AssertPopupIsShown(IElement? cookiesPopup)
    {
        Assert.Contains(cookiesPopup!.ClassList, c => c == "fixed");
    }

    private static void AssertPopupIsNotShown(IElement? cookiesPopup)
    {
        Assert.Contains(cookiesPopup!.ClassList, c => c == "hidden");
    }
}