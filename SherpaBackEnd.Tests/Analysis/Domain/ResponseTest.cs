

using SherpaBackEnd.Analysis.Domain;

namespace SherpaBackEnd.Tests.Analysis.Domain;

public class ResponseTest
{

    [Fact]
    public void ShouldReturnTrueIfResponseIsPositiveWithOddNumberOfOptions()
    {
        var options = new List<string>(){"1","2","3","4","5"};
        bool actual = new Response<string>("Real Team","4",false, options).IsPositive();
        Assert.True(actual);
    }

    [Fact]
    public void ShouldReturnTrueIfResponseIsPostiveWithEvenNumberOfOptions()
    {
        var options = new List<string>(){"1","2","3","4"};
        bool actual = new Response<string>("Real Team","3",false, options).IsPositive();
        Assert.True(actual);
    }

    [Fact]
    public void ShouldReturnFalseIfResponseIsNotPositiveWithOddNumberOfOptions()
    {
        var options = new List<string>(){"1","2","3","4","5"};
        bool actual = new Response<string>("Real Team","2",false, options).IsPositive();
        Assert.False(actual);
    }
    
    [Fact]
    public void ShouldReturnFalseIfResponseIsNotPositiveWithEvenNumberOfOptions()
    {
        var options = new List<string>(){"1","2","3","4"};
        bool actual = new Response<string>("Real Team","2",false, options).IsPositive();
        Assert.False(actual);
    }

    [Fact]
    public void ShouldReturnTrueIfResponseIsPositiveWithEvenNumberOfReverseOptions()
    {
        var options = new List<string>(){"1","2","3","4"};
        bool actual = new Response<string>("Real Team","2",true, options).IsPositive();
        Assert.True(actual);
    }
    
    [Fact]
    public void ShouldReturnTrueIfResponseIsPositiveWithOddNumberOfReverseOptions()
    {
        var options = new List<string>(){"1","2","3","4","5"};
        bool actual = new Response<string>("Real Team","1",true, options).IsPositive();
        Assert.True(actual);
    }
}