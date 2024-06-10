using Amazon.SimpleEmail;
using Testcontainers.LocalStack;

namespace SherpaBackEnd.Tests.Integration;

public class SESEmailServiceTest : IAsyncDisposable
{
    LocalStackContainer localStackContainer = new LocalStackBuilder().Build();


    public async Task InitialiseSES()
    {
        await localStackContainer.StartAsync()
            .ConfigureAwait(false);
    }

    [Fact]
    public async Task METHOD()
    {
        await InitialiseSES();
        var config = new AmazonSimpleEmailServiceConfig();
        config.ServiceURL = localStackContainer.GetConnectionString();
        using var client = new AmazonSimpleEmailServiceClient(config);
    }

    public async ValueTask DisposeAsync()
    {
        await localStackContainer.DisposeAsync();
    }
}