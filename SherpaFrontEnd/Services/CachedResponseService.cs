using Blazored.LocalStorage;

namespace SherpaFrontEnd.Services;

public class CachedResponseService : ICachedResponseService
{
    private readonly ILocalStorageService _localStorageService;

    public CachedResponseService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public Task<Dictionary<int, string>> GetBy(Guid surveyNotificationId)
    {
        
        return Task.FromResult(new Dictionary<int, string>());
    }
}