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
}