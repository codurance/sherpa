using Blazored.Toast.Configuration;
using Blazored.Toast.Services;
using Moq;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class BlazoredToastServiceTest
{

    [Fact]
    public void ShouldShowASuccessToastNotification()
    {
        var toastMock = new Mock<IToastService>();
        
        var blazoredToastService = new BlazoredToastService(toastMock.Object);

        blazoredToastService.ShowSuccess("Test 1");
        
        toastMock.Verify(service => service.ShowSuccess("Test 1", It.IsAny<Action<ToastSettings>>()));
    }

    [Fact]
    public void ShouldShowAnErrorToastNotification()
    {
        var toastMock = new Mock<IToastService>();
        
        var blazoredToastService = new BlazoredToastService(toastMock.Object);

        blazoredToastService.ShowError("Test 1 error");
        
        toastMock.Verify(service => service.ShowError("Test 1 error", It.IsAny<Action<ToastSettings>>()));
    }
}