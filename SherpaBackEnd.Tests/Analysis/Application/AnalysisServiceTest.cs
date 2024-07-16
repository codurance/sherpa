using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Analysis.Application;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Domain.Exceptions;
using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Helpers.Analysis;
using SherpaBackEnd.Survey.Domain.Persistence;

namespace SherpaBackEnd.Tests.Analysis.Application;

public class AnalysisServiceTest
{
    [Fact]
    public async Task ShouldBeAbleToRetrieveGeneralResultsFromATeamId()
    {
        // Given
        var expected = GeneralResultsDtoBuilder.Build();

        var teamId = Guid.NewGuid();
        var surveyRepository = new Mock<ISurveyRepository>();
        surveyRepository.Setup(repository => repository.GetAllSurveysWithResponsesFromTeam(teamId))
            .ReturnsAsync(new List<Survey.Domain.Survey>
            {
            });
        var templateAnalysisRepository = new Mock<IAnalysisRepository>();
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
        CustomAssertions.StringifyEquals(expected, actual);

        // Surveys


        // ColumnCharts
    }

    [Fact]
    public async Task ShouldThrowTeamNotFoundErrorWhenTeamIdIsNotFound()
    {
        var teamId = Guid.NewGuid();
        var templateAnalysisRepository = new Mock<IAnalysisRepository>();
        var surveyRepository = new Mock<ISurveyRepository>();

        surveyRepository.Setup(repository => repository.GetAllSurveysWithResponsesFromTeam(teamId))
            .ThrowsAsync(new TeamNotFoundException("Team ID is not found"));
        
        var analysisService = new AnalysisService(surveyRepository.Object, templateAnalysisRepository.Object);
        var exceptionThrown = await Assert.ThrowsAsync<TeamNotFoundException>(async () =>
            await analysisService.GetGeneralResults(teamId));
        Assert.IsType<TeamNotFoundException>(exceptionThrown);
    }
}