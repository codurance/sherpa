using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class DeliverySettingsTest
{
    private TestContext _ctx;
    private Mock<ITeamDataService> _teamService;
    private FakeNavigationManager _navMan;
    private readonly List<Team>? _teams;


    public DeliverySettingsTest()
    {
        _ctx = new TestContext();
        _teamService = new Mock<ITeamDataService>();
        _ctx.Services.AddScoped( _=> _teamService.Object);
        _navMan = _ctx.Services.GetRequiredService<FakeNavigationManager>();
        _teams = new List<Team>() {new Team(Guid.NewGuid(), "Demo Team")};
    }

    [Fact]
    public async Task ShouldRenderSettingsForm()
    {
        _teamService.Setup(service => service.GetAllTeams()).ReturnsAsync(_teams);
        
        var component = _ctx.RenderComponent<DeliverySettings>( ComponentParameter.CreateParameter("Template", "Hackman Model"));

        var teamSelect = component
            .FindAll("select")
            .FirstOrDefault(element => element.InnerHtml.Contains("Demo Team"));
        Assert.NotNull(teamSelect);

        var titleLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Title"));

        var titleInput = component.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(titleInput);

        var descriptionLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Description"));

        var descriptionTextArea = component.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(descriptionTextArea);

        var deadlineLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Deadline"));

        var deadlineInput = component.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(deadlineInput);

        Assert.NotNull(component.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue")));
    }
}