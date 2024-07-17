using Bunit;
using SherpaFrontEnd.Pages.TeamContent.Graphs;

namespace BlazorApp.Tests.Pages.Graphs;

public class GeneralResultsColumnChartTest
{
    private TestContext _testContext;

    public GeneralResultsColumnChartTest()
    {
        _testContext = new TestContext();
        _testContext.JSInterop.SetupVoid("generateColumnsChart", _ => true);
    }

    [Fact]
    public void ShouldRenderGeneralResultsColumnChart()
    {
        var generalResultsDto = AnalysisHelper.BuildGeneralResultsDto();

        var generalresultsColumnChart =
            _testContext.RenderComponent<GeneralResultsColumnChart>(ComponentParameter.CreateParameter("GeneralResults", generalResultsDto));
        
        var jsRuntimeInvocation = _testContext.JSInterop.Invocations.ToList().Find(invocation => invocation.Identifier.Equals("generateColumnsChart"));
        Assert.NotNull(jsRuntimeInvocation.Identifier);
        Assert.Equal(generalResultsDto.ColumnChart, jsRuntimeInvocation.Arguments[1]);
        Assert.Equal(generalResultsDto.Metrics.General, jsRuntimeInvocation.Arguments[2]);
        var generalResultsColumnChartId = "general-results-column-chart";
        Assert.Contains(generalResultsColumnChartId, jsRuntimeInvocation.Arguments);
        var divToRenderColumnChart = generalresultsColumnChart.Find($"div[id='{generalResultsColumnChartId}']");
        Assert.NotNull(divToRenderColumnChart);
    }
}