using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SherpaFrontEnd.Services;

public class AuthService : IAuthService
{
    private readonly IAccessTokenProvider _tokenProvider;

    public AuthService(IAccessTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task<HttpRequestMessage> DecorateWithToken(HttpRequestMessage request)
    {
        var requestAccessToken = await _tokenProvider.RequestAccessToken();
        requestAccessToken.TryGetToken(out var token);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

        return request;
    }
}