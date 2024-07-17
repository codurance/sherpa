using Shared.Test.Helpers;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Tests.Helpers.Analysis;

namespace SherpaBackEnd.Tests.Analysis.Infrastructure.Dto;

public class GeneralResultsDtoTest
{
    [Fact]
    public void ShouldCreateAGeneralResultsDtoFromAHackmanAnalysis()
    {
        var surveys = AnalysisHelper.BuildSurveyResponses();
        var analysis = new HackmanAnalysis(surveys);
        var actual = GeneralResultsDto.FromAnalysis(analysis);
        var expected = AnalysisHelper.BuildGeneralResultsDto();
        CustomAssertions.StringifyEquals(expected, actual);
    }
}