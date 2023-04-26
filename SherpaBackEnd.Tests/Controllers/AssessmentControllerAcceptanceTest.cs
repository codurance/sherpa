using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class AssessmentControllerAcceptanceTest
{
    private readonly InMemorySurveyRepository _inMemorySurveyRepository;
    private readonly InMemoryAssessmentRepository _inMemoryAssessmentRepository;
    private readonly SurveyService _surveyService;
    private readonly AssessmentsController _assessmentsController;

    public AssessmentControllerAcceptanceTest()
    {
        _inMemorySurveyRepository = new InMemorySurveyRepository();
        _inMemoryAssessmentRepository = new InMemoryAssessmentRepository();
        _surveyService = new SurveyService(_inMemorySurveyRepository, _inMemoryAssessmentRepository);
        _assessmentsController = new AssessmentsController(_surveyService);
    }

    [Fact]
    public async Task AddNewAssessmentWithExistingTemplate_OkResultExpected()
    {
        
        var templates = await _assessmentsController.GetTemplates();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var assessment = new Assessment(Guid.NewGuid(), actualTemplates.First().Id, "Assessment A");
        var assessmentRequest = await  _assessmentsController.AddAssessment(assessment);
        
        var assessmentResult = Assert.IsType<OkObjectResult>(assessmentRequest.Result);
        var actualAssessment = Assert.IsAssignableFrom<Assessment>(assessmentResult.Value);
        Assert.Equal(assessment.GroupId, actualAssessment.GroupId);
        Assert.Equal(assessment.TemplateId, actualAssessment.TemplateId);
        Assert.Equal(assessment.Name, actualAssessment.Name);
        Assert.Empty(actualAssessment.Surveys);
    }
    
    [Fact]
    public async Task AddNewAssessmentWithNonExistingTemplate_BadRequestExpected()
    {
        
        var templates = await _assessmentsController.GetTemplates();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var assessment = await  _assessmentsController.AddAssessment(new Assessment(Guid.NewGuid(), Guid.NewGuid(), "Assessment A"));
        
        Assert.IsType<BadRequestResult>(assessment.Result);
    }
}