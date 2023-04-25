using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class AssessmentControllerAcceptanceTest
{
    
    [Fact]
    public async Task Test()
    {
        var surveyRepository = new SurveyRepository();
        var surveyService = new SurveyService(surveyRepository);
        var assessmentController = new AssessmentController(surveyService);
        
        var templates = await assessmentController.GetTemplates();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var groupId = Guid.NewGuid();
        var templateId = actualTemplates.First().Id;
        var assessment = await  assessmentController.AddAssessment(groupId, templateId);
        
        var assessmentResult = Assert.IsType<OkObjectResult>(assessment.Result);
        var actualAssessment = Assert.IsAssignableFrom<Assessment>(assessmentResult.Value);
        Assert.Equal(groupId, actualAssessment.GroupId);
        Assert.Equal(templateId, actualAssessment.TemplateId);
        Assert.Empty(actualAssessment.Surveys);
    }
}