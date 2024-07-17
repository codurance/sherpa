using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Tests.Helpers.Analysis;

namespace SherpaBackEnd.Tests.Analysis.Domain;

public class HackmanAnalysisTest
{
    [Fact]
    public void ShouldBeAbleToCreateAHackmanAnalysisWithCategories()
    {
        var responses = new List<QuestionResponse>()
        {
            new(1, "1"),
            new(2, "1"),
            new(3, "1"),
            new(4, "1"),
            new(5, "1"),
            new(6, "1")
        };
        var template = new TemplateAnalysis("Hackman template", new Dictionary<int, Question>()
        {
            [1] = new("Real Team", null, 1, false),
            [2] = new("Compelling Direction", null, 1, false),
            [3] = new("Expert Coaching", null, 1, false),
            [4] = new("Enable Structure", null, 1, false),
            [5] = new("Supportive Coaching", null, 1, false),
            [6] = new("Real Team", null, 1, false),
        });
        var surveys = new List<SurveyResponses>() { new(responses, template) };
        var expected = AnalysisHelper.GetHackmanCategories();

        var actual = new HackmanAnalysis(surveys).Categories;

        Assert.Equal(expected, actual);
    }
    
}