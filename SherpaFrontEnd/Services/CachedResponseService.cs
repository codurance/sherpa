using Blazored.LocalStorage;

namespace SherpaFrontEnd.Services;

public class CachedResponseService : ICachedResponseService
{
    private readonly ILocalStorageService _localStorageService;

    public CachedResponseService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<Dictionary<int, string>> GetBy(Guid surveyNotificationId)
    {
        var cachedResponses = await _localStorageService.GetItemAsync<Dictionary<int, string>>($"response-{surveyNotificationId}");
        return cachedResponses ?? new Dictionary<int, string>();
    }

    public Task Save(Guid surveyNotificationId, Dictionary<int, string> responses)
    {
        throw new NotImplementedException();
    }
}