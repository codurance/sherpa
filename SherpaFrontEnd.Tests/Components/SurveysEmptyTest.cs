using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using SherpaFrontEnd.Core.Components;

namespace BlazorApp.Tests.Components;

public class SurveysEmptyTest
{
    private TestContext _testContext;
    private readonly FakeNavigationManager _navManager;

    public SurveysEmptyTest()
    {
        _testContext = new TestContext();
        _navManager = _testContext.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public void ShouldShowTextIntroducedAsParameters()
    {
        var titleText = "You don’t have any surveys yet";
        var descriptionText = "Let's begin the journey towards a stronger, more effective team!";
        var buttonText = "Launch first survey";
        var appComponent = _testContext.RenderComponent<SurveysEmpty>(
            ComponentParameter.CreateParameter("Title", titleText),
            ComponentParameter.CreateParameter("Description", descriptionText),
            ComponentParameter.CreateParameter("Button", buttonText)
        );

        var titleElement = appComponent.FindElementByCssSelectorAndTextContent("p", titleText);
        Assert.NotNull(titleElement);
        var descriptionElement = appComponent.FindElementByCssSelectorAndTextContent("p", descriptionText);
        Assert.NotNull(descriptionElement);
        var buttonElement = appComponent.FindElementByCssSelectorAndTextContent("button", buttonText);
        Assert.NotNull(buttonElement);
    }

    [Fact]
    public void ShouldRedirectToCreateSurveyPageWhenButtonClicked()
    {
        var appComponent = _testContext.RenderComponent<SurveysEmpty>();

        var launchSurveyButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch first survey");
        Assert.NotNull(launchSurveyButton);
        launchSurveyButton.Click();

        appComponent.WaitForAssertion(() =>
            Assert.Equal("http://localhost/survey/delivery-settings?template=Hackman%20Model", _navManager.Uri));
    }
}