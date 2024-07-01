using Blazored.LocalStorage;

namespace SherpaFrontEnd.Services;

public class CookiesService: ICookiesService
{
    private readonly ILocalStorageService _localStorageService;
    private const string LocalStorageKey = "CookiesAcceptedDate";

    public CookiesService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<bool> AreCookiesAccepted()
    {
        var cookiesAcceptedDate = await _localStorageService.GetItemAsync<long?>(LocalStorageKey);

        if (cookiesAcceptedDate == null)
        {
            return false;
        }

        var acceptedDateOlderThanSixMonths =
            DateTimeOffset.FromUnixTimeMilliseconds((long)cookiesAcceptedDate).AddMonths(6) < DateTimeOffset.Now;

        if (acceptedDateOlderThanSixMonths)
        {
            return false;
        }

        return true;
    }

    public async ValueTask AcceptCookies()
    {
        await _localStorageService.SetItemAsync(LocalStorageKey, DateTimeOffset.Now.ToUnixTimeMilliseconds());
    }
}