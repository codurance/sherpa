using Microsoft.AspNetCore.Components;

namespace SherpaFrontEnd.Services;

public class NavigationService : INavigationService
{
    private NavigationManager _navigationManager;

    public NavigationService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public string CurrentUri => _navigationManager.Uri;

    public void NavigateTo(string uri)
    {
        _navigationManager.NavigateTo(uri);
    }
}