using Moq;
using SherpaBackEnd.Analysis.Application;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Survey.Domain.Persistence;

namespace SherpaBackEnd.Tests.Analysis.Application;

public class AnalysisServiceTest
{
    [Fact]
    public async Task ShouldBeAbleToRetrieveGeneralResultsFromATeamId()
    {
        // Given
        var categories = new[]
            { "Real Team", "Compelling Direction", "Expert Coaching", "Enable Structure", "Supportive Coaching" };
        var survey1 = new ColumnSeries<double>("Survey 1", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });
        var survey2 = new ColumnSeries<double>("Survey 2", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });

        var series = new List<ColumnSeries<double>>() { survey1, survey2 };
        var columnChart = new ColumnChart<double>(categories, series, new ColumnChartConfig<double>(1,0.25,2));
        var metrics = new Metrics(0.9,0.75);
        var expected = new GeneralResultsDto(columnChart, metrics);

        var teamId = Guid.NewGuid();
        var surveyRepository = new Mock<ISurveyRepository>();
        surveyRepository.Setup(repository => repository.GetAllSurveysWithResponsesFromTeam(teamId))
            .ReturnsAsync(new List<Survey.Domain.Survey>());
        var templateAnalysisRepository = new Mock<ITemplateAnalysisRepository>();
        var templateName = "Hackman Model";

        var questions = new Dictionary<int, Question>()
        {
            [0] = new Question("Real team", "Delimited", 0, false),
            [1] = new Question("Expert Coaching", "Extent and focus of coaching provided by peers.", 1, true),
            [1] = new Question("Real team", "Interdependent", 2, false),
        };
        templateAnalysisRepository.Setup(repository => repository.GetTemplateAnalysisByName(templateName))
            .ReturnsAsync(new TemplateAnalysis("Hackman Model", questions));

        var analysisService = new AnalysisService(surveyRepository.Object, templateAnalysisRepository.Object);

        // When
        var actual = await analysisService.GetGeneralResults(teamId);

        // Then
        Assert.Equal(expected, actual);

        // Surveys
        

        // ColumnCharts
    }
}