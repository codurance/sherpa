using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Tests.Helpers.Analysis;

namespace SherpaBackEnd.Tests.Analysis.Domain;

public class HackmanAnalysisTest
{
    [Fact]
    public void ShouldBeAbleToCreateAHackmanAnalysisWithCategories()
    {
        var options = new List<string>() { "1", "2", "3", "4", "5" };
        var responses = new List<Response<string>>()
        {
            new("Real Team", "1", false, options),
            new("Compelling Direction", "1", false, options),
            new("Expert Coaching", "1", false, options),
            new("Enable Structure", "1", false, options),
            new("Supportive Coaching", "1", false, options),
            new("Real Team", "1", false, options),
        };
        var participants = new List<Participant<string>> { new(responses) };

        var template = new TemplateAnalysis("Hackman template", new Dictionary<int, Question>()
        {
            [1] = new("Real Team", null, 1, false),
            [2] = new("Compelling Direction", null, 1, false),
            [3] = new("Expert Coaching", null, 1, false),
            [4] = new("Enable Structure", null, 1, false),
            [5] = new("Supportive Coaching", null, 1, false),
            [6] = new("Real Team", null, 1, false),
        });
        var surveys = new List<SurveyResponses<string>>() { new("Survey 1", participants, template) };
        var expected = AnalysisHelper.GetHackmanCategories();

        var actual = new HackmanAnalysis(surveys).Categories;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldGroupResultsBySurveys()
    {
        var options = new List<string>() { "1", "2", "3", "4", "5" };
        var responses = new List<Response<string>>()
        {
            new("Real Team", "1", false, options),
            new("Compelling Direction", "1", false, options),
            new("Expert Coaching", "1", false, options),
            new("Enable Structure", "1", false, options),
            new("Supportive Coaching", "1", false, options),
            new("Real Team", "1", false, options),
        };
        var participants = new List<Participant<string>> { new(responses) };

        var template = new TemplateAnalysis("Hackman template", new Dictionary<int, Question>()
        {
            [1] = new("Real Team", null, 1, false),
            [2] = new("Compelling Direction", null, 1, false),
            [3] = new("Expert Coaching", null, 1, false),
            [4] = new("Enable Structure", null, 1, false),
            [5] = new("Supportive Coaching", null, 1, false),
            [6] = new("Real Team", null, 1, false),
        });
        var surveyTitle = "Survey 1";
        var surveyResponses = new SurveyResponses<string>(surveyTitle, participants, template);
        var surveys = new List<SurveyResponses<string>>() { surveyResponses };
        var surveyResult = new SurveyResult<string>(surveyTitle);
        surveyResult.Categories = AnalysisHelper.GetHackmanCategories();
        var expected = new List<SurveyResult<string>>() { surveyResult };

        var actual = new HackmanAnalysis(surveys).Surveys;

        Assert.Equal(expected.Count, actual.Count);
        Assert.Equal(expected.First().Categories, actual.First().Categories);
    }
}