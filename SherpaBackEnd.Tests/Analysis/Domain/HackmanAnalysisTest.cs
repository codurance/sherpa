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
            new("Enabling Structure", "1", false, options),
            new("Supportive Organizational Context", "1", false, options),
            new("Real Team", "1", false, options),
        };
        var participants = new List<Participant<string>> { new(responses) };
        
        var surveys = new List<SurveyAnalysisData<string>>() { new("Survey 1", participants) };
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
            new("Enabling Structure", "1", false, options),
            new("Supportive Organizational Context", "1", false, options),
            new("Real Team", "1", false, options),
        };
        var participants = new List<Participant<string>> { new(responses) };
        
        var surveyTitle = "Survey 1";
        var surveyResponses = new SurveyAnalysisData<string>(surveyTitle, participants);
        var surveys = new List<SurveyAnalysisData<string>>() { surveyResponses };
        var surveyResult = new SurveyResult<string>(surveyTitle);
        surveyResult.Categories = AnalysisHelper.GetHackmanCategories();
        var expected = new List<SurveyResult<string>>() { surveyResult };

        var actual = new HackmanAnalysis(surveys).Surveys;

        Assert.Equal(expected.Count, actual.Count);
        Assert.Equal(expected.First().Categories, actual.First().Categories);
    }
    
    [Fact]
    public void ShouldReturnAverageZeroWithZeroSurveys()
    {

        var actual = new HackmanAnalysis(new List<SurveyAnalysisData<string>>());

        Assert.Equal(0, actual.Average);
    }
}