namespace SherpaFrontEnd.Services;

public interface INavigationService
{
    string CurrentUri { get; }
    void NavigateTo(string uri);
}