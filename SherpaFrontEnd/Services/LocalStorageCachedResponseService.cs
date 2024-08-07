using Blazored.LocalStorage;

namespace SherpaFrontEnd.Services;

public class LocalStorageCachedResponseService : ICachedResponseService
{
    private readonly ILocalStorageService _localStorageService;

    public LocalStorageCachedResponseService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<Dictionary<int, string>> GetBy(Guid surveyNotificationId)
    {
        var cachedResponses = await _localStorageService.GetItemAsync<Dictionary<int, string>>($"response-{surveyNotificationId}");
        return cachedResponses ?? new Dictionary<int, string>();
    }

    public async Task Save(Guid surveyNotificationId, Dictionary<int, string> responses)
    {
        await _localStorageService.SetItemAsync($"response-{surveyNotificationId}", responses);
    }

    public async Task Clear(Guid surveyNotificationId)
    {
        await _localStorageService.RemoveItemAsync($"response-{surveyNotificationId}");
    }
}