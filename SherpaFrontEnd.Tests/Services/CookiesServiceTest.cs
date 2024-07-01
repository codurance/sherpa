using Blazored.LocalStorage;
using Moq;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class CookiesServiceTest
{

    [Fact]
    public async Task ShouldReturnCookiesNotAcceptedIfThereIsNoEntry()
    {
        var localStorageMock = new Mock<ILocalStorageService>();
        
        localStorageMock.Setup(mock => mock.GetItemAsync<string>("CookiesAcceptedDate", It.IsAny<CancellationToken>())).ReturnsAsync((string?)null);
        
        var cookiesService = new CookiesService(localStorageMock.Object);
        var areCookiesAccepted = await cookiesService.AreCookiesAccepted();
        
        localStorageMock.Verify(mock => mock.GetItemAsync<string>("CookiesAcceptedDate", It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.False(areCookiesAccepted);
    }

    [Fact]
    public void ShouldSetCookiesAcceptedEntry()
    {
        var localStorageMock = new Mock<ILocalStorageService>();
        
        var cookiesService = new CookiesService(localStorageMock.Object);
        cookiesService.AcceptCookies();
        
        localStorageMock.Verify(mock => mock.SetItemAsync("CookiesAcceptedDate", It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}