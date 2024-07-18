using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.Helpers.Analysis;

public static class AnalysisHelper
{
    public static GeneralResultsDto BuildGeneralResultsDto()
    {
        var data = new List<double>() { 0.5, 0.5, 0.33, 0, 1};
        var categories = GetHackmanCategories();
        var survey1 = new ColumnSeries<double>("Survey 1", data);
        var survey2 = new ColumnSeries<double>("Survey 2", data);
        var survey3 = new ColumnSeries<double>("Survey 3", data);
        var survey4 = new ColumnSeries<double>("Survey 4", data);
        var survey5 = new ColumnSeries<double>("Survey 5", data);

        var series = new List<ColumnSeries<double>>() { survey1, survey2, survey3, survey4, survey5 };
        var columnChart = new ColumnChart<double>(categories, series, new ColumnChartConfig<double>(1, 0.1, 1));
        var generalMetrics = new GeneralMetrics(0.47, 0.75);
        var metrics = new Metrics(generalMetrics);
        return new GeneralResultsDto(columnChart, metrics);
    }

    public static GeneralResultsDto BuildASingleSurveyGeneralResultsDto()
    {
        var data = new List<double>() { 0.5, 1.0, 0.75, 1.0, 1.0 };
        var categories = GetHackmanCategories();
        var survey = new ColumnSeries<double>("Super Survey", data);

        var series = new List<ColumnSeries<double>>() { survey };
        var columnChart = new ColumnChart<double>(categories, series, new ColumnChartConfig<double>(1, 0.1, 1));
        var generalMetrics = new GeneralMetrics(0.85, 0.75);
        var metrics = new Metrics(generalMetrics);
        return new GeneralResultsDto(columnChart, metrics);
    }

    public static List<SurveyAnalysisData<string>> BuildSurveyResponses()
    {
        return new List<SurveyAnalysisData<string>>
        {
            new("Survey 1", BuildParticipants()),
            new("Survey 2", BuildParticipants()),
            new("Survey 3", BuildParticipants()),
            new("Survey 4", BuildParticipants()),
            new("Survey 5", BuildParticipants()),
        };
    }

    public static List<SurveyAnalysisData<string>> BuildASurveyWithMultipleParticipants()
    {
        var options = new List<string>() { "1", "2", "3", "4", "5" };

        return new List<SurveyAnalysisData<string>>()
        {
            new("Super Survey", new List<Participant<string>>()
            {
                new(new List<Response<string>>()
                {
                    new("Real Team", "1", false, options),
                    new("Enabling Structure", "5", false, options),
                    new("Enabling Structure", "5", false, options),
                    new("Compelling Direction", "5", false, options),
                    new("Expert Coaching", "5", false, options),
                    new("Supportive Organizational Context", "5", false, options),
                }),
                new(new List<Response<string>>()
                {
                    new("Real Team", "5", false, options),
                    new("Enabling Structure", "1", false, options),
                    new("Enabling Structure", "5", false, options),
                    new("Compelling Direction", "5", false, options),
                    new("Expert Coaching", "5", false, options),
                    new("Supportive Organizational Context", "5", false, options),
                })
            })
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
                new("Enabling Structure", "5", false, options),
                new("Supportive Organizational Context", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "5", false, options),
                new("Compelling Direction", "5", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enabling Structure", "5", false, options),
                new("Supportive Organizational Context", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "5", false, options),
                new("Compelling Direction", "5", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enabling Structure", "1", false, options),
                new("Supportive Organizational Context", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "1", false, options),
                new("Compelling Direction", "1", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enabling Structure", "1", false, options),
                new("Supportive Organizational Context", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "1", false, options),
                new("Compelling Direction", "1", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enabling Structure", "1", false, options),
                new("Supportive Organizational Context", "5", true, options),
            }),
            new(new List<Response<string>>
            {
                new("Real Team", "1", false, options),
                new("Compelling Direction", "1", false, options),
                new("Expert Coaching", "5", false, options),
                new("Enabling Structure", "1", false, options),
                new("Supportive Organizational Context", "5", true, options),
            })
        };
    }

    public static List<string> GetHackmanCategories()
    {
        return new List<string>()
        {
            "Real Team",
            "Compelling Direction",
            "Enabling Structure",
            "Supportive Organizational Context",
            "Expert Coaching"
        };
    }
}