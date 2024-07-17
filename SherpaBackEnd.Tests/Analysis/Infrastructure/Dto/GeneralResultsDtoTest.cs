using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Tests.Helpers.Analysis;

namespace SherpaBackEnd.Tests.Analysis.Infrastructure.Dto;

public class GeneralResultsDtoTest
{
    [Fact]
    public void ShouldCreateAGeneralResultsDtoFromAHackmanAnalysis()
    {
        var surveys = new List<SurveyResponses<string>>();
        var analysis = new HackmanAnalysis(surveys);
        var actual = GeneralResultsDto.FromAnalysis(analysis);
        var expected = AnalysisHelper.BuildGeneralResultsDto();
        Assert.Equal(expected, actual);
    }
}