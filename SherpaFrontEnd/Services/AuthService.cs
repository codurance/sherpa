using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SherpaFrontEnd.Services;

public class AuthService : IAuthService
{
    private readonly IAccessTokenProvider _tokenProvider;
    private NavigationManager _navigationManager;

    public AuthService(IAccessTokenProvider tokenProvider, NavigationManager navigationManager)
    {
        _tokenProvider = tokenProvider;
        _navigationManager = navigationManager;
    }

    public async Task<HttpRequestMessage> DecorateWithToken(HttpRequestMessage request)
    {
        var requestAccessToken = await _tokenProvider.RequestAccessToken();
        requestAccessToken.TryGetToken(out var token);
        if (token == null)
        {
            _navigationManager.NavigateTo("/authentication/logged-out");
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

        return request;
    }
}