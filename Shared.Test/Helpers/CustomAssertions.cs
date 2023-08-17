using Newtonsoft.Json;
using Xunit;

namespace Shared.Test.Helpers;

public static class CustomAssertions
{
    public static void StringifyEquals(object? expected, object actual)
    {
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
    } 
}