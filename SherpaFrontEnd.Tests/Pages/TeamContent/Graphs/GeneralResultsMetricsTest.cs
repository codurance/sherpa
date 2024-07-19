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
    public void ShouldRenderSurveyAverageMetric()
    {
        var generalResultsDto = AnalysisHelper.BuildGeneralResultsDto();

        var generalresultsColumnChart =
            _testContext.RenderComponent<GeneralResultsMetrics>(ComponentParameter.CreateParameter("MetricsData", generalResultsDto.Metrics));
        
        var firstSurveyMetricName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "[SURVEY 1]");
        Assert.NotNull(firstSurveyMetricName);
        var firstSurveyMetricAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "50%");
        Assert.NotNull(firstSurveyMetricAverage);
        var secondSurveyMetricName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "[SURVEY 2]");
        Assert.NotNull(secondSurveyMetricName);
        var secondSurveyMetricAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "66%");
        Assert.NotNull(secondSurveyMetricAverage);
        var thirdSurveyMetricName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "[SURVEY 3]");
        Assert.NotNull(thirdSurveyMetricName);
        var thirdSurveyMetricAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "70%");
        Assert.NotNull(thirdSurveyMetricAverage);
        var fourthSurveyMetricName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "[SURVEY 4]");
        Assert.NotNull(fourthSurveyMetricName);
        var fourthSurveyMetricAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "83%");
        Assert.NotNull(fourthSurveyMetricAverage);
    }

    [Fact]
    public void ShouldRenderCategoriesAverageMetric()
    {
        var generalResultsDto = AnalysisHelper.BuildGeneralResultsDto();

        var generalresultsColumnChart =
            _testContext.RenderComponent<GeneralResultsMetrics>(ComponentParameter.CreateParameter("MetricsData", generalResultsDto.Metrics));
        
        var firstSurveyCategoryName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "Real team");
        Assert.NotNull(firstSurveyCategoryName);
        var firstSurveyCategoryAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "74%");
        Assert.NotNull(firstSurveyCategoryAverage);
        
        var secondSurveyCategoryName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "Enable Structure");
        Assert.NotNull(secondSurveyCategoryName);
        var secondSurveyCategoryAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "68%");
        Assert.NotNull(secondSurveyCategoryAverage);
        
        var thirdSurveyCategoryName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "Expert coaching");
        Assert.NotNull(thirdSurveyCategoryName);
        var thirdSurveyCategoryAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "81%");
        Assert.NotNull(thirdSurveyCategoryAverage);
        
        var fourthSurveyCategoryName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "Supportive org coaching");
        Assert.NotNull(fourthSurveyCategoryName);
        var fourthSurveyCategoryAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "88%");
        Assert.NotNull(fourthSurveyCategoryAverage);
        
        var fifthSurveyCategoryName = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "Compelling direction");
        Assert.NotNull(fifthSurveyCategoryName);
        var fifthSurveyCategoryAverage = generalresultsColumnChart.FindElementByCssSelectorAndTextContent("p", "64%");
        Assert.NotNull(fifthSurveyCategoryAverage);
    }
}