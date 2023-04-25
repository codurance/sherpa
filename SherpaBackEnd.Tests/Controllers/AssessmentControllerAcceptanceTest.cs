using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class AssessmentControllerAcceptanceTest
{
    private readonly SurveyRepository _surveyRepository;
    private readonly SurveyService _surveyService;
    private readonly AssessmentsController _assessmentsController;

    public AssessmentControllerAcceptanceTest()
    {
        _surveyRepository = new SurveyRepository();
        _surveyService = new SurveyService(_surveyRepository);
        _assessmentsController = new AssessmentsController(_surveyService);
    }

    [Fact]
    public async Task AddNewAssessmentWithExistingTemplate_OkResultExpected()
    {
        
        var templates = await _assessmentsController.GetTemplates();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var groupId = Guid.NewGuid();
        var templateId = actualTemplates.First().Id;
        var assessment = await  _assessmentsController.AddAssessment(groupId, templateId);
        
        var assessmentResult = Assert.IsType<OkObjectResult>(assessment.Result);
        var actualAssessment = Assert.IsAssignableFrom<Assessment>(assessmentResult.Value);
        Assert.Equal(groupId, actualAssessment.GroupId);
        Assert.Equal(templateId, actualAssessment.TemplateId);
        Assert.Empty(actualAssessment.Surveys);
    }
    
    [Fact]
    public async Task AddNewAssessmentWithNonExistingTemplate_BadRequestExpected()
    {
        
        var templates = await _assessmentsController.GetTemplates();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var groupId = Guid.NewGuid();
        var templateId = Guid.NewGuid();
        var assessment = await  _assessmentsController.AddAssessment(groupId, templateId);
        
        Assert.IsType<BadRequestResult>(assessment.Result);
    }
}