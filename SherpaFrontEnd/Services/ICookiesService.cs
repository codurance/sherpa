namespace SherpaFrontEnd.Services;

public interface ICookiesService
{
    Task<bool> AreCookiesAccepted();
    ValueTask AcceptCookies();
}