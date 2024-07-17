using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.Helpers.Analysis;

public static class AnalysisHelper
{
    public static GeneralResultsDto BuildGeneralResultsDto()
    {
        var data = new List<double>() { 0.5, 0.5, 1, 0.33, 0 };
        var categories = GetHackmanCategories();
        var survey1 = new ColumnSeries<double>("Survey 1", data);
        var survey2 = new ColumnSeries<double>("Survey 2", data);
        var survey3 = new ColumnSeries<double>("Survey 3", data);
        var survey4 = new ColumnSeries<double>("Survey 4", data);
        var survey5 = new ColumnSeries<double>("Survey 5", data);

        var series = new List<ColumnSeries<double>>() { survey1, survey2, survey3, survey4, survey5 };
        var columnChart = new ColumnChart<double>(categories, series, new ColumnChartConfig<double>(1, 0.25, 2));
        var generalMetrics = new GeneralMetrics(0.75, 0.75);
        var metrics = new Metrics(generalMetrics);
        return new GeneralResultsDto(columnChart, metrics);
    }

    public static List<SurveyResponses<string>> BuildSurveyResponses()
    {

        var questions = new Dictionary<int, Question>()
        {
            [0] = new("Real team", "Delimited", 0, false),
            [1] = new("Expert Coaching", "Extent and focus of coaching provided by peers.", 1, true),
            [1] = new("Real team", "Interdependent", 2, false),
        };
        var templateAnalysis = new TemplateAnalysis("Hackman template", questions);
        
        
        return new List<SurveyResponses<string>>
        {
            new("Survey 1", BuildParticipants(), templateAnalysis),
            new("Survey 2", BuildParticipants(), templateAnalysis),
            new("Survey 3", BuildParticipants(), templateAnalysis),
            new("Survey 4", BuildParticipants(), templateAnalysis),
            new("Survey 5", BuildParticipants(), templateAnalysis),
        };
    }

    public static List<Participant<string>> BuildParticipants()
    {
        var options = new List<string>() { "1", "2", "3", "4", "5" };

        return new List<Participant<string>>
        {
            new(new List<Response<string>>
            {
                new("Real Team", "5", false, options),
                new("Compelling Direction", "5", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enable Structure", "5", false, options),
                new("Supportive Coaching", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "5", false, options),
                new("Compelling Direction", "5", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enable Structure", "5", false, options),
                new("Supportive Coaching", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "5", false, options),
                new("Compelling Direction", "5", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enable Structure", "1", false, options),
                new("Supportive Coaching", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "1", false, options),
                new("Compelling Direction", "1", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enable Structure", "1", false, options),
                new("Supportive Coaching", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "1", false, options),
                new("Compelling Direction", "1", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enable Structure", "1", false, options),
                new("Supportive Coaching", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "1", false, options),
                new("Compelling Direction", "1", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enable Structure", "1", false, options),
                new("Supportive Coaching", "5", true, options),
            })
        };
    }

    public static List<string> GetHackmanCategories()
    {
        return new List<string>()
            { "Real Team", "Compelling Direction", "Expert Coaching", "Enable Structure", "Supportive Coaching" };
    }
}