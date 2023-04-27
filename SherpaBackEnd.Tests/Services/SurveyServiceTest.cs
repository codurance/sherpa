using Moq;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Services;

public class SurveyServiceTest
{
    private readonly Mock<ISurveyRepository> _mockSurveyRepository;
    private readonly Mock<IAssessmentRepository> _mockAssessmentRepository;
    private readonly AssessmentService _service;

    public SurveyServiceTest()
    {
        _mockSurveyRepository = new Mock<ISurveyRepository>();
        _mockAssessmentRepository = new Mock<IAssessmentRepository>();
        _service = new AssessmentService(_mockSurveyRepository.Object, _mockAssessmentRepository.Object);
    }

    [Fact]
    public async Task GetTemplates_ReturnsNonEmptyList()
    {
        var template = new SurveyTemplate("hackman");

        _mockSurveyRepository.Setup(m => m.GetTemplates())
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
        _mockSurveyRepository.Verify(m => m.GetTemplates());
    }

    [Fact]
    public void AddNewAssessment_ReturnsNewAssessment()
    {
        var templateId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        const string name = "Assessment A";

        var assessment = new Assessment(groupId, templateId, name);
        _mockAssessmentRepository.Setup(m => m.GetAssessment(groupId, templateId))
            .Returns(assessment);
        _mockSurveyRepository.Setup(m => m.IsTemplateExist(templateId))
            .Returns(true);
        
        var actualAssessment = _service.AddAssessment(groupId, templateId, name);
        
        Assert.Equal(groupId, actualAssessment.GroupId);
        Assert.Equal(templateId, actualAssessment.TemplateId);
        Assert.Empty(actualAssessment.Surveys);
    }
}