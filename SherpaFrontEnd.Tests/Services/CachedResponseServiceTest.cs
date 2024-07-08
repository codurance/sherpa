using Blazored.LocalStorage;
using Moq;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class CachedResponseServiceTest
{
    [Fact]
    public async Task ShouldReturnAnEmptyDictionaryWhenThereAreNoCachedResponses()
    {
        var localStorageMock = new Mock<ILocalStorageService>();

        var sut = new CachedResponseService(localStorageMock.Object);

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
        var sut = new CachedResponseService(localStorageMock.Object);

        var actual = await sut.GetBy(surveyNotificationId);
        Assert.Equal(expected, actual);
    }
}