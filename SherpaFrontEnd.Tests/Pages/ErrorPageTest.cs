
using Bunit;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Pages.ErrorPage;

namespace BlazorApp.Tests.Pages;

public class ErrorPageTest
{
    [Fact]
    public void ShouldDisplayDefaultErrorMessage()
    {
        var testContext = new TestContext();
        const string errorMessage = "Something went wrong.";
        
        var errorPageComponent = testContext.RenderComponent<ErrorPage>();
        var errorMessageElement = errorPageComponent.FindElementByCssSelectorAndTextContent("p", errorMessage);
        Assert.NotNull(errorMessageElement);
    }
}