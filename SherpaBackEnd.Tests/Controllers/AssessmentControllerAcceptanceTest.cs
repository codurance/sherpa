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
    private readonly AssessmentService _assessmentService;
    private readonly AssessmentsController _assessmentsController;

    public AssessmentControllerAcceptanceTest()
    {
        _inMemorySurveyRepository = new InMemorySurveyRepository();
        _inMemoryAssessmentRepository = new InMemoryAssessmentRepository();
        _assessmentService = new AssessmentService(_inMemorySurveyRepository, _inMemoryAssessmentRepository);
        _assessmentsController = new AssessmentsController(_assessmentService);
    }

    [Fact]
    public async Task AddNewAssessmentWithExistingTemplate_OkResultExpected()
    {
        
        var templates = await _assessmentsController.GetTemplatesAsync();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var assessment = new Assessment(Guid.NewGuid(), actualTemplates.First().Id, "Assessment A");
        var assessmentRequest = await  _assessmentsController.AddAssessmentAsync(assessment);
        
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
        
        var templates = await _assessmentsController.GetTemplatesAsync();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var assessment = await  _assessmentsController.AddAssessmentAsync(new Assessment(Guid.NewGuid(), Guid.NewGuid(), "Assessment A"));
        
        Assert.IsType<BadRequestResult>(assessment.Result);
    }

    [Fact]
    public async Task AddNewSurveyToNonExistingAssessment_NotFoundExpected()
    {
        var groupId = Guid.NewGuid();
        var templateId = Guid.NewGuid();
        var assessmentToUpdate = new Assessment(groupId, templateId, "test assessment");
        // _inMemoryAssessmentRepository.AddAssessment(assessmentToUpdate);

        var updateAssessment = await _assessmentsController.UpdateAssessmentAsync(assessmentToUpdate);

        Assert.IsType<NotFoundResult>(updateAssessment.Result);
    }
}