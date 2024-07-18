using Bunit;
using SherpaFrontEnd.Pages.TeamContent.Graphs;

namespace BlazorApp.Tests.Pages.Graphs;

public class GeneralResultsMetricsTest
{
    private readonly TestContext _testContext;

    public GeneralResultsMetricsTest()
    {
        _testContext = new TestContext();
    }

    [Fact]
    public void ShouldRenderTeamAverageMetricForEachSurvey()
    {
        var generalResultsDto = AnalysisHelper.BuildGeneralResultsDto();

        var generalresultsColumnChart =
            _testContext.RenderComponent<GeneralResultsMetrics>(ComponentParameter.CreateParameter("MetricsData", generalResultsDto.Metrics));
        //TODO finish assertions
    }
}