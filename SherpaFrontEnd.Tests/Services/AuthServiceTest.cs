using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Moq;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class AuthServiceTest
{

    [Fact]
    public async Task ShouldAddTokenToRequest()
    {
        var tokenProviderMock = new Mock<IAccessTokenProvider>();
        var expectedToken = new AccessToken()
        {
            Value = "test"
        };
        var accessTokenMock = new AccessTokenResult(AccessTokenResultStatus.Success, expectedToken, "");
        tokenProviderMock.Setup(tokenProvider => tokenProvider.RequestAccessToken()).ReturnsAsync(accessTokenMock);
        
        var authService = new AuthService(tokenProviderMock.Object, null);

        var request = new HttpRequestMessage();
        request = await authService.DecorateWithToken(request);
        
        Assert.Equal(expectedToken.Value, request.Headers.Authorization?.Parameter);
    }
    
    [Fact]
    public async Task ShouldRedirectToLoginPageWhenTokenIsExpired1()
    {
        var tokenProviderMock = new Mock<IAccessTokenProvider>();
        var navigationService = new Mock<INavigationService>();

        var accessTokenMock = new AccessTokenResult(AccessTokenResultStatus.RequiresRedirect, null, "");
        tokenProviderMock.Setup(tp => tp.RequestAccessToken()).ReturnsAsync(accessTokenMock);
        navigationService.SetupGet(ns => ns.CurrentUri).Returns("http://localhost");

        var authService = new AuthService(tokenProviderMock.Object, navigationService.Object);

        await authService.DecorateWithToken(new HttpRequestMessage());
    
        navigationService.Verify(service => service.NavigateTo(It.Is<string>(uri => uri.Contains("authentication/login"))), Times.Once);
    }
    
}