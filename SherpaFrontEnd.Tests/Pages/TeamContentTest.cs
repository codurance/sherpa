using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class TeamContentTest
{
    private TestContext _testContext;
    private IRenderedComponent<TeamContent> _renderedComponent;
    private Mock<ITeamDataService> _mockTeamService;
    private Mock<IAssessmentsDataService> _mockAssessmentService;

    public TeamContentTest()
    {
        _testContext = new TestContext();
        _mockTeamService = new Mock<ITeamDataService>();
        _mockAssessmentService = new Mock<IAssessmentsDataService>();
        _testContext.Services.AddScoped(s => _mockTeamService.Object);
        _testContext.Services.AddScoped(s => _mockAssessmentService.Object);
    }

    [Fact]
    public void SingleTeamIsRendered()
    {
        var team = new Team
        {
            Name = "Team A",
            Id = Guid.NewGuid()
        };

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        _renderedComponent = _testContext.RenderComponent<TeamContent>();

        Assert.Equal(team.Name, _renderedComponent.Instance.Team!.Name);
    }
}