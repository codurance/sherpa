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
        navigationService.SetupGet(ns => ns.CurrentUri).Returns("http://localhost/teams-list");

        var authService = new AuthService(tokenProviderMock.Object, navigationService.Object);

        await authService.DecorateWithToken(new HttpRequestMessage());

        var expectedLoginUrl = "authentication/login?returnUrl=http%3A%2F%2Flocalhost%2Fteams-list";
        navigationService.Verify(service => service.NavigateTo(expectedLoginUrl), Times.Once);
    }
}