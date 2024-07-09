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
    
}