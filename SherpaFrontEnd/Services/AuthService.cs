using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SherpaFrontEnd.Services;

public class AuthService : IAuthService
{
    private readonly IAccessTokenProvider _tokenProvider;
    private INavigationService  _navigationService;

    public AuthService(IAccessTokenProvider tokenProvider, INavigationService navigationService)
    {
        _tokenProvider = tokenProvider;
        _navigationService = navigationService;
    }

    public async Task<HttpRequestMessage> DecorateWithToken(HttpRequestMessage request)
    {
        var requestAccessToken = await _tokenProvider.RequestAccessToken();
        requestAccessToken.TryGetToken(out var token);
        if (token == null)
        {
            _navigationService.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(_navigationService.CurrentUri)}");
            return request;
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

        return request;
    }
}