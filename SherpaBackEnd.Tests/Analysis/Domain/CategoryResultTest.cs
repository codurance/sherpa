using SherpaBackEnd.Analysis.Domain;

namespace SherpaBackEnd.Tests.Analysis.Domain;

public class CategoryResultTest
{
    [Fact]
    public void ShouldCalculatePercentageOfPositives()
    {
        var categoryResult = new CategoryResult()
        {
            NumberOfPositives = 5,
            TotalResponses = 10
        };
        Assert.Equal(0.5, categoryResult.PercentageOfPositives);
    }
}