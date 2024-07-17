using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Analysis.Application;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Domain.Exceptions;
using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Tests.Helpers.Analysis;

namespace SherpaBackEnd.Tests.Analysis.Application;

public class AnalysisServiceTest
{
    [Fact]
    public async Task ShouldBeAbleToRetrieveGeneralResultsFromATeamId()
    {
        // Given
        var expected = AnalysisHelper.BuildGeneralResultsDto();

        var teamId = Guid.NewGuid();

        var analysisRepository = new Mock<IAnalysisRepository>();
        var templateName = "Hackman Model";

        // var questions = new Dictionary<int, Question>()
        // {
        //     [0] = new("Real team", "Delimited", 0, false),
        //     [1] = new("Expert Coaching", "Extent and focus of coaching provided by peers.", 1, true),
        //     [1] = new("Real team", "Interdependent", 2, false),
        // };

        var surveys = new List<SurveyResponses<string>>();

        analysisRepository.Setup(repository => repository.GetAnalysisByTeamIdAndTemplateName(teamId, templateName))
            .ReturnsAsync(new HackmanAnalysis(surveys));

        var analysisService = new AnalysisService(analysisRepository.Object);

        // When
        var actual = await analysisService.GetGeneralResults(teamId);

        // Then
        CustomAssertions.StringifyEquals(expected, actual);
    }

    [Fact]
    public async Task ShouldThrowTeamNotFoundErrorWhenTeamIdIsNotFound()
    {
        var teamId = Guid.NewGuid();
        var analysisRepository = new Mock<IAnalysisRepository>();

        analysisRepository
            .Setup(repository => repository.GetAnalysisByTeamIdAndTemplateName(teamId, "Hackman template"))
            .ThrowsAsync(new TeamNotFoundException("Team ID is not found"));

        var analysisService = new AnalysisService(analysisRepository.Object);
        var exceptionThrown = await Assert.ThrowsAsync<TeamNotFoundException>(async () =>
            await analysisService.GetGeneralResults(teamId));
        Assert.IsType<TeamNotFoundException>(exceptionThrown);
    }
}