using Bunit;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Pages;

public class ErrorPageTest
{
    [Fact]
    public void ShouldDisplayDefaultErrorMessage()
    {
        var testContext = new TestContext();
        const string errorMessage = "Something went wrong.";
        
        var errorPageComponent = testContext.RenderComponent<ErrorPage>();
        var errorMessageElement = errorPageComponent.FindAll("p").FirstOrDefault(element => element.InnerHtml.Contains(errorMessage));
        
        Assert.NotNull(errorMessageElement);
    }
}