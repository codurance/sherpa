using Blazored.LocalStorage;
using Moq;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class LocalStorageCachedResponseServiceTest
{
    [Fact]
    public async Task ShouldReturnAnEmptyDictionaryWhenThereAreNoCachedResponses()
    {
        var localStorageMock = new Mock<ILocalStorageService>();

        var sut = new LocalStorageCachedResponseService(localStorageMock.Object);

        var actual = await sut.GetBy(Guid.NewGuid());
        var expected = new Dictionary<int, string>();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ShouldReturnCachedResponses()
    {
        var surveyNotificationId = Guid.NewGuid();

        var localStorageMock = new Mock<ILocalStorageService>();

        var expected = new Dictionary<int, string>()
        {
            { 1, "ENG_1" },
            { 2, "ENG_3" },
            { 3, "ENG_1" },
        };
        
        localStorageMock
            .Setup(mock =>
                mock.GetItemAsync<Dictionary<int, string>?>($"response-{surveyNotificationId}", CancellationToken.None))
            .ReturnsAsync(expected);
        var sut = new LocalStorageCachedResponseService(localStorageMock.Object);

        var actual = await sut.GetBy(surveyNotificationId);
        Assert.Equal(expected, actual);
    }
    
    
    [Fact]
    public async Task ShouldSaveResponsesInLocalStorage()
    {
        var localStorageMock = new Mock<ILocalStorageService>();
        var localStorageCachedResponseService = new LocalStorageCachedResponseService(localStorageMock.Object);

        var responses = new Dictionary<int, string>()
        {
            { 1, "ENG_1" },
            { 2, "ENG_3" },
            { 3, "ENG_1" },
        };

        var surveyNotificationId = Guid.NewGuid();
        await localStorageCachedResponseService.Save(surveyNotificationId, responses);
        
        localStorageMock.Verify(localStorage => localStorage.SetItemAsync($"response-{surveyNotificationId}", responses, CancellationToken.None));
    }
}