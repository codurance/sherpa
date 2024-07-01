using AngleSharp.Dom;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared;

namespace BlazorApp.Tests.Shared;

public class CookiesPopupTest
{

    [Fact]
    public void ShouldNotShowIfCookiesAreAlreadyAccepted()
    {
        var testContext = new TestContext();
        var mockCookiesService = new Mock<ICookiesService>();

        mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(true);
        
        testContext.Services.AddScoped<ICookiesService>(sp => mockCookiesService.Object);
        
        var cut = testContext.RenderComponent<CookiesPopup>();
        
        var cookiesPopup = cut.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]", "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
        
        mockCookiesService.Verify(mock => mock.AreCookiesAccepted(), Times.Once);
        
        AssertPopupIsNotShown(cookiesPopup);
    }

    [Fact]
    public void ShouldShowIfCookiesAreNotAccepted()
    {
        var testContext = new TestContext();
        var mockCookiesService = new Mock<ICookiesService>();

        mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(false);
        
        testContext.Services.AddScoped<ICookiesService>(sp => mockCookiesService.Object);
        
        var cut = testContext.RenderComponent<CookiesPopup>();
        
        var cookiesPopup = cut.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]", "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
        
        mockCookiesService.Verify(mock => mock.AreCookiesAccepted(), Times.Once);
        
        AssertPopupIsShown(cookiesPopup);
    }

    [Fact]
    public void ShouldSetCookiesAcceptedWhenButtonIsClicked()
    {
        var testContext = new TestContext();
        var mockCookiesService = new Mock<ICookiesService>();
        
        mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(false);
        
        testContext.Services.AddScoped<ICookiesService>(sp => mockCookiesService.Object);
        
        var cut = testContext.RenderComponent<CookiesPopup>();

        var button = cut.FindElementByCssSelectorAndTextContent("button", "Accept");

        button!.Click();
        
        cut.WaitForAssertion(() =>
        {
            mockCookiesService.Verify(mock => mock.AcceptCookies(), Times.Once);
        });
    }

    [Fact]
    public void ShouldHideAfterAcceptingCookies()
    {
        var testContext = new TestContext();
        var mockCookiesService = new Mock<ICookiesService>();
        
        mockCookiesService.Setup(mock => mock.AreCookiesAccepted()).ReturnsAsync(false);
        
        testContext.Services.AddScoped<ICookiesService>(sp => mockCookiesService.Object);
        
        var cut = testContext.RenderComponent<CookiesPopup>();

        var button = cut.FindElementByCssSelectorAndTextContent("button", "Accept");

        button!.Click();
        
        cut.WaitForAssertion(() =>
        {
            var cookiesPopup = cut.FindElementByCssSelectorAndTextContent("div[role=\"dialog\"]", "To find out more about our Cookies policy here. Once you are done, please, come back and accept them.");
            AssertPopupIsNotShown(cookiesPopup);
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