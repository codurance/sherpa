using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Services;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Pages;

public class DeliverySettingsTest
{
    private TestContext _ctx;
    private Mock<ITeamService> _teamService;
    private FakeNavigationManager _navMan;
    private readonly Team[] _teams;

    public DeliverySettingsTest()
    {
        _ctx = new TestContext();
        _teamService = new Mock<ITeamService>();
        _ctx.Services.AddSingleton<ITeamService>(_teamService.Object);
        _navMan = _ctx.Services.GetRequiredService<FakeNavigationManager>();
        _teams = new []{new Team(Guid.NewGuid(), "Demo Team")};
    }

    [Fact]
    public async Task ShouldRenderSettingsForm()
    {
        _teamService.Setup(service => service.GetAllTeamsAsync()).ReturnsAsync(_teams);
        _navMan.NavigateTo($"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}");
        var component = _ctx.RenderComponent<DeliverySettings>();

        var teamSelect = component
            .FindAll("select")
            .FirstOrDefault(element => element.InnerHtml.Contains("Demo Team"));
        Assert.NotNull(teamSelect);
        Assert.True(teamSelect.IsRequired());

        var titleLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Title"));

        var titleInput = component.Find($"input#{titleLabel!.Attributes.GetNamedItem("for")}");

        Assert.NotNull(titleInput);
        Assert.True(titleInput.IsRequired());

        var descriptionLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Description"));

        var descriptionTextArea = component.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for")}");
        Assert.NotNull(descriptionTextArea);
        Assert.False(descriptionTextArea.IsRequired());

        var deadlineLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Deadline"));

        var deadlineInput = component.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for")}");

        Assert.NotNull(deadlineInput);
        Assert.False(deadlineInput.IsRequired());

        Assert.NotNull(component.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue")));
    }
}