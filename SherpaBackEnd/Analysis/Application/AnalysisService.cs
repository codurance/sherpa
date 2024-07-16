using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Survey.Domain.Persistence;

namespace SherpaBackEnd.Analysis.Application;

public class AnalysisService : IAnalysisService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ITemplateAnalysisRepository _templateAnalysisRepository;

    public AnalysisService(ISurveyRepository surveyRepository, ITemplateAnalysisRepository templateAnalysisRepository)
    {
        _surveyRepository = surveyRepository;
        _templateAnalysisRepository = templateAnalysisRepository;
    }

    public async Task<GeneralResultsDto> GetGeneralResults(Guid teamId)
    {
        // var categories = new[]
        //     { "Real Team", "Compelling Direction", "Expert Coaching", "Enable Structure", "Supportive Coaching" };
        // var survey1 = new ColumnSeries<double>("Survey 1", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });
        // var survey2 = new ColumnSeries<double>("Survey 2", new List<double>() { 0.5, 0.6, 0.2, 0.4, 0.7 });
        // var survey3 = new ColumnSeries<double>("Survey 3", new List<double>() { 0.6, 0.6, 0.3, 0.5, 0.6 });
        // var survey4 = new ColumnSeries<double>("Survey 4", new List<double>() { 0.6, 0.6, 0.5, 0.5, 0.8 });
        // var survey5 = new ColumnSeries<double>("Survey 5", new List<double>() { 0.8, 0.7, 0.5, 0.5, 0.9 });
        //
        // var series = new List<ColumnSeries<double>>() { survey1, survey2, survey3, survey4, survey5 };
        // var columnChart = new ColumnChart<double>(categories, series, new ColumnChartConfig<double>(1,0.25,2));
        // var generalMetrics = new GeneralMetrics(0.9, 0.75);
        // var metrics = new Metrics(generalMetrics);
        // var generalResults = new GeneralResultsDto(columnChart, metrics);
        //
        // return generalResults;
        
        // 1- Get the surveys
        // 2- Get the TemplateAnalysis
        // 3- We iterate the surveys and their responses
        
        // for each category we count the positives
        // mapper than creates the general-results
        
        // 1 collaborator that takes a list of surveys and returns a general-results object
        
        // -> for each response we categorize it
        // 4- Count all the participants and divide the positive results by them
        // 5- Build the ColumnChart

        await _surveyRepository.GetAllSurveysWithResponsesFromTeam(teamId);
        
        throw new NotImplementedException();
    }
}