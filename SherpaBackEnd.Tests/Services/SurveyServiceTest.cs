using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;
using SherpaBackEnd.Services.Email;

namespace SherpaBackEnd.Tests.Services;

public class SurveyServiceTest
{
    private readonly Mock<ISurveyRepository> _mockSurveyRepository;
    private readonly Mock<IAssessmentRepository> _mockAssessmentRepository;
    private readonly Mock<IEmailService> _emailService;
    private readonly AssessmentService _service;

    public SurveyServiceTest()
    {
        _mockSurveyRepository = new Mock<ISurveyRepository>();
        _mockAssessmentRepository = new Mock<IAssessmentRepository>();
        _emailService = new Mock<IEmailService>();
        _service = new AssessmentService(_mockSurveyRepository.Object, _mockAssessmentRepository.Object, _emailService.Object);
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
    public async Task AddNewAssessment_ReturnsNewAssessment()
    {
        var templateId = Guid.NewGuid();
        var teamId = Guid.NewGuid();
        const string name = "Assessment A";

        var assessment = new Assessment(teamId, templateId, name);
        _mockAssessmentRepository.Setup(m => m.GetAssessment(teamId, templateId))
            .ReturnsAsync(assessment);
        _mockSurveyRepository.Setup(m => m.IsTemplateExist(templateId))
            .Returns(true);
        
        var actualAssessment = await _service.AddAssessment(teamId, templateId, name);
        
        Assert.Equal(teamId, actualAssessment!.TeamId);
        Assert.Equal(templateId, actualAssessment.TemplateId);
        Assert.Empty(actualAssessment.Surveys);
    }

    [Fact]
    public async Task UpdateAssessment_ReturnsUpdatedAssessment()
    {
        var templateId = Guid.NewGuid();
        var teamId = Guid.NewGuid();
        const string name = "Assessment A";

        var assessment = new Assessment(teamId, templateId, name);
        _mockAssessmentRepository.Setup(m => m.GetAssessment(teamId, templateId))
            .ReturnsAsync(assessment);

        var survey = new DeprecatedSurvey(DateOnly.FromDateTime(DateTime.Now), new List<string>());
        var surveys = new List<DeprecatedSurvey> { survey };
        assessment.Surveys = surveys;

        _mockAssessmentRepository.Setup(m => m.UpdateAssessment(assessment))
            .ReturnsAsync(assessment);

        var updatedAssessment = await _service.UpdateAssessment(assessment);
        Assert.NotEmpty(updatedAssessment.Surveys);
        Assert.Single(updatedAssessment.Surveys);
    }

    [Fact]
    public async Task ShouldCallSurveyRepositoryToGetAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveyRepository = new Mock<ISurveyRepository>();
        var surveyService = new SurveyService(surveyRepository.Object);

        await surveyService.GetAllSurveysFromTeam(teamId);
        
        surveyRepository.Verify(_ => _.GetAllSurveysFromTeam(teamId), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowErrorIfConnectionToRepositoryNotSuccessful()
    {
        var teamId = Guid.NewGuid();
        var surveyRepository = new Mock<ISurveyRepository>();
        var surveyService = new SurveyService(surveyRepository.Object);

        surveyRepository.Setup(_ => _.GetAllSurveysFromTeam(teamId)).ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () => await surveyService.GetAllSurveysFromTeam(teamId));
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }
}