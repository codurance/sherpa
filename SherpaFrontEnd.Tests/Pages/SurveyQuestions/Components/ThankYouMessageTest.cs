using Bunit;
using SherpaBackEnd.Template.Domain;
using SherpaFrontEnd.Pages.SurveyQuestions.Components;

namespace BlazorApp.Tests.Pages.Components;

public class ThankYouMessageTest
{
    private TestContext _testContext;
    private IRenderedComponent<ThankYouMessage> _renderedComponent;

    public ThankYouMessageTest()
    {
        _testContext = new TestContext();
    }

    [Fact]
    public void ShouldDisplayThankYouMessageInSpanishAfterSurveyCompletion()
    {
        var thankYouMessage =
            _testContext.RenderComponent<ThankYouMessage>(
                ComponentParameter.CreateParameter("SelectedLanguage", Languages.SPANISH));
        
        var thankYouTitleInSpanish = thankYouMessage.FindElementByCssSelectorAndTextContent("h1", "¡Gracias por tu respuesta!");
        Assert.NotNull(thankYouTitleInSpanish);
    }
    
    [Fact]
    public void ShouldDisplayThankYouMessageInEnglishAfterSurveyCompletion()
    {
        var thankYouMessage =
            _testContext.RenderComponent<ThankYouMessage>(
                ComponentParameter.CreateParameter("SelectedLanguage", Languages.ENGLISH));
        
        var thankYouTitleInEnglish = thankYouMessage.FindElementByCssSelectorAndTextContent("h1", "Thank you for your reply!");
        Assert.NotNull(thankYouTitleInEnglish);
    }

}