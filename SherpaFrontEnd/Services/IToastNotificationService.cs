namespace SherpaFrontEnd.Services;

public interface IToastNotificationService
{
    void ShowSuccess(string message);
    void ShowError(string message);
}