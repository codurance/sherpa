using System.Collections;
using System.Runtime.CompilerServices;
using Moq;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Services;

public class SurveyServiceTest
{
    private readonly Mock<ISurveyRepository> _mockRepository;
    private readonly SurveyService _service;

    public SurveyServiceTest()
    {
        _mockRepository = new Mock<ISurveyRepository>();
        _service = new SurveyService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetTemplates_ReturnsNonEmptyList()
    {
        var template = new SurveyTemplate("hackman");

        _mockRepository.Setup(m => m.GetTemplates())
            .ReturnsAsync(new List<SurveyTemplate> { template });
        var actualTemplates = await _service.GetTemplates();
        var templatesList = actualTemplates.ToList();

        Assert.NotEmpty(templatesList);
        Assert.Equal(template.Name, templatesList.First().Name);
    }

    [Fact]
    public async Task GetTemplates_InvokesRepository()
    {
        var templates = await _service.GetTemplates();
        _mockRepository.Verify(m => m.GetTemplates());
    }
}