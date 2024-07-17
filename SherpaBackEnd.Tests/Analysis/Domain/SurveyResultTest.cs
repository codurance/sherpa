using SherpaBackEnd.Analysis.Domain;

namespace SherpaBackEnd.Tests.Analysis.Domain;

public class SurveyResultTest
{
    [Fact]
    public void ShouldAddAPositiveResponseToASingleCategory()
    {
        var expected = new Dictionary<string, CategoryResult>()
        {
            ["Real Team"] = new()
            {
                NumberOfPositives = 1
            }
        };

        var actual = new SurveyResult<string>("survey 1");;
        var options = new List<string>(){"1","2","3","4","5"};
        actual.AddResponse(new Response<string>("Real Team", "5", false, options));
        Assert.Equal(expected["Real Team"].NumberOfPositives, actual.CategoryResults["Real Team"].NumberOfPositives);
    }
    
    [Fact]
    public void ShouldAddMultiplePositiveResponseToASingleCategory()
    {
        var expected = new Dictionary<string, CategoryResult>()
        {
            ["Real Team"] = new()
            {
                NumberOfPositives = 3
            }
        };

        var actual = new SurveyResult<string>("survey 1");;
        var options = new List<string>(){"1","2","3","4","5"};
        actual.AddResponse(new Response<string>("Real Team", "5", false, options));
        actual.AddResponse(new Response<string>("Real Team", "5", false, options));
        actual.AddResponse(new Response<string>("Real Team", "5", false, options));
        Assert.Equal(expected["Real Team"].NumberOfPositives, actual.CategoryResults["Real Team"].NumberOfPositives);
    }
    
    [Fact]
    public void ShouldAddMultiplePositiveResponseToMultipleCategories()
    {
        var expected = new Dictionary<string, CategoryResult>()
        {
            ["Real Team"] = new()
            {
                NumberOfPositives = 1
            },
            ["Expert Coaching"] = new()
            {
                NumberOfPositives = 2
            }
        };

        var actual = new SurveyResult<string>("survey 1");;
        var options = new List<string>(){"1","2","3","4","5"};
        actual.AddResponse(new Response<string>("Real Team", "5", false, options));
        actual.AddResponse(new Response<string>("Expert Coaching", "5", false, options));
        actual.AddResponse(new Response<string>("Expert Coaching", "5", false, options));
        Assert.Equal(expected["Real Team"].NumberOfPositives, actual.CategoryResults["Real Team"].NumberOfPositives);
    }
}