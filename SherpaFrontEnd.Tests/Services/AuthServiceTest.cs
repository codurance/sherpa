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
        var navigationManagerMock = new Mock<FakeNavigationManager>();
        var expectedToken = new AccessToken()
        {
            Value = "test"
        };
        var accessTokenMock = new AccessTokenResult(AccessTokenResultStatus.Success, expectedToken, "");
        tokenProviderMock.Setup(tokenProvider => tokenProvider.RequestAccessToken()).ReturnsAsync(accessTokenMock);
        
        var authService = new AuthService(tokenProviderMock.Object, navigationManagerMock.Object);

        var request = new HttpRequestMessage();
        request = await authService.DecorateWithToken(request);
        
        Assert.Equal(expectedToken.Value, request.Headers.Authorization?.Parameter);
    }


    [Fact(Skip = "")]

    public async Task ShouldRedirectToLoginPageWhenTokenIsExpired()
    {
        var tokenProviderMock = new Mock<IAccessTokenProvider>();
        var navManagerMock = new Mock<NavigationManager>();
        var accessTokenMock = new AccessTokenResult(AccessTokenResultStatus.RequiresRedirect, null, "");
        var authService = new AuthService(tokenProviderMock.Object, navManagerMock.Object);
        tokenProviderMock.Setup(tokenProvider => tokenProvider.RequestAccessToken()).ReturnsAsync(accessTokenMock);

        await authService.DecorateWithToken(new HttpRequestMessage());
        navManagerMock.Verify(navManger => navManger.NavigateTo("/authentication/logged-out", It.IsAny<bool>()));
    }
}