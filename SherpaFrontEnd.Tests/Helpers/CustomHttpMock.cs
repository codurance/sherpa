using Moq;
using Moq.Protected;

namespace BlazorApp.Tests.Helpers;

public static class CustomHttpMock
{
    public static void SetupRequest(this Mock<HttpMessageHandler> handler, HttpMethod method, string path,
        params HttpResponseMessage[] responses)
    {
        var setupSequentialResult = handler
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(method) && m.RequestUri!.AbsoluteUri.EndsWith(path)),
                ItExpr.IsAny<CancellationToken>());
        
        foreach (var httpResponseMessage in responses)
        {
            setupSequentialResult
                .ReturnsAsync(httpResponseMessage);
        }
    }
    
    public static void SetupRequestWithException(this Mock<HttpMessageHandler> handler, HttpMethod method, string path,
        Exception exception)
    {
        handler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(method) && m.RequestUri!.AbsoluteUri.EndsWith(path)),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(exception);
    }
}