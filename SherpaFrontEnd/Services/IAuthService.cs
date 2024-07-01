namespace SherpaFrontEnd.Services;

public interface IAuthService
{
    Task<HttpRequestMessage> DecorateWithToken(HttpRequestMessage request);
}