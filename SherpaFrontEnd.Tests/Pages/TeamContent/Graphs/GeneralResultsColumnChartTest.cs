using Bunit;
using SherpaFrontEnd.Dtos.Analysis;
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
        var generalResultsDto = SetupGeneralResultsDto();

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
    
    private GeneralResultsDto SetupGeneralResultsDto()
    {
        var categories = new string[]
        {
            "Real team",
            "Compelling direction",
            "Expert coaching",
            "Enable structure",
            "Supportive org coaching"
        };
        var columnChartConfig = new ColumnChartConfig<double>(1.0, 0.25, 2);
        var firstSurvey = new ColumnSeries<double>("Survey 1", new List<double>()
        {
            0.5,
            0.5,
            0.5,
            0.5,
            0.5
        });
        var secondSurvey = new ColumnSeries<double>("Survey 2", new List<double>()
        {
            0.5,
            0.5,
            0.5,
            0.5,
            0.5
        });
        var series = new List<ColumnSeries<double>>() { firstSurvey, secondSurvey };
        var columnChart = new ColumnChart<double>(categories, series, columnChartConfig);

        var generalMetrics = new GeneralMetrics(0.50, 0.75);
        var metrics = new Metrics(generalMetrics);
        
        var generalResults = new GeneralResultsDto(columnChart, metrics);
        return generalResults;
    }
}