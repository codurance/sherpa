using Blazored.Toast.Services;

namespace SherpaFrontEnd.Services;

public class BlazoredToastService : IToastNotificationService
{
    private readonly IToastService _toastService;

    public BlazoredToastService(IToastService toastService)
    {
        _toastService = toastService;
    }

    public void ShowSuccess(string message)
    {
        _toastService.ShowSuccess(message);
    }

    public void ShowError(string message)
    {
        throw new NotImplementedException();
    }
}