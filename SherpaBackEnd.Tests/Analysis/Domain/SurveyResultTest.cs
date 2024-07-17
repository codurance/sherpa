using SherpaBackEnd.Analysis.Domain;

namespace SherpaBackEnd.Tests.Analysis.Domain;

public class SurveyResultTest
{
    [Fact]
    public void ShouldAddResponsesToCategories()
    {
        var expected = new Dictionary<string, CategoryResult>()
        {
            ["Real Team"] = new()
            {
                NumberOfPositives = 1
            },
        };

        var actual = new SurveyResult<string>("survey 1");;
        var options = new List<string>(){"1","2","3","4","5"};
        actual.AddResponse(new Response<string>("Real Team", "5", false, options));
        Assert.Equal(expected, actual.CategoryResults);
    }
}