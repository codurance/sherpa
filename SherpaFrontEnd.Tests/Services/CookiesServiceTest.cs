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
        
        localStorageMock.Setup(mock => mock.GetItemAsync<long?>("CookiesAcceptedDate", It.IsAny<CancellationToken>())).ReturnsAsync((long?)null);
        
        var cookiesService = new CookiesService(localStorageMock.Object);
        var areCookiesAccepted = await cookiesService.AreCookiesAccepted();
        
        localStorageMock.Verify(mock => mock.GetItemAsync<long?>("CookiesAcceptedDate", It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.False(areCookiesAccepted);
    }

    [Fact]
    public async Task ShouldReturnCookiesNotAcceptedIfEntryIsOlderThanSixMonths()
    {
        var localStorageMock = new Mock<ILocalStorageService>();
        
        var sevenMonthsAgo = DateTimeOffset.Now.AddMonths(-7).ToUnixTimeMilliseconds();
        localStorageMock.Setup(mock => mock.GetItemAsync<long?>("CookiesAcceptedDate", It.IsAny<CancellationToken>())).ReturnsAsync(sevenMonthsAgo);
        
        var cookiesService = new CookiesService(localStorageMock.Object);
        var areCookiesAccepted = await cookiesService.AreCookiesAccepted();
        
        localStorageMock.Verify(mock => mock.GetItemAsync<long?>("CookiesAcceptedDate", It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.False(areCookiesAccepted);
    }

    [Fact]
    public async Task ShouldReturnCookiesAcceptedIfEntryExistsAndIsWithinSixMonths()
    {
        var localStorageMock = new Mock<ILocalStorageService>();
        
        var fiveMonthsAgo = DateTimeOffset.Now.AddMonths(-5).ToUnixTimeMilliseconds();
        localStorageMock.Setup(mock => mock.GetItemAsync<long?>("CookiesAcceptedDate", It.IsAny<CancellationToken>())).ReturnsAsync(fiveMonthsAgo);
        
        var cookiesService = new CookiesService(localStorageMock.Object);
        var areCookiesAccepted = await cookiesService.AreCookiesAccepted();
        
        localStorageMock.Verify(mock => mock.GetItemAsync<long?>("CookiesAcceptedDate", It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.True(areCookiesAccepted);
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