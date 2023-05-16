using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;
using SherpaBackEnd.Services.Email;

namespace SherpaBackEnd.Tests.Controllers;
using SherpaBackEnd.Controllers;

public class SurveysControllerAcceptanceTest
{
    private InMemorySurveyRepository _surveyRepository;
    private IAssessmentRepository _assessmentRepository;
    private Mock<IEmailService> _emailService;
    private AssessmentService _asseptanceService;
    private AssessmentsController _assessmentsController;

    private ISurveysService _surveysService;
    private SurveysController _surveysController;

    public SurveysControllerAcceptanceTest()
    {
        _surveyRepository = new InMemorySurveyRepository();
        _assessmentRepository = new InMemoryAssessmentRepository();
        _emailService = new Mock<IEmailService>();
        _asseptanceService = new AssessmentService(_surveyRepository, _assessmentRepository, _emailService.Object);
        _assessmentsController = new AssessmentsController(_asseptanceService);

        _surveysService = new SurveysService(_surveyRepository);
        _surveysController = new SurveysController(_surveysService);
    }

    [Fact]
    public async Task Test()
    {
        var templatesRequest = await _assessmentsController.GetTemplatesAsync();
        var templatesResult = Assert.IsType<OkObjectResult>(templatesRequest.Result);
        var actualTemplates = Assert.IsAssignableFrom<List<SurveyTemplate>>(templatesResult.Value);
        
        var actualQuestionsRequest = await _surveysController.GetQuestions(actualTemplates.First().Id);
        var actualQuestionsResult = Assert.IsType<OkObjectResult>(actualQuestionsRequest.Result);
        var actualQuestions = Assert.IsAssignableFrom<List<QuestionContent>>(actualQuestionsResult.Value);

        Assert.NotEmpty(actualQuestions);
        
    }
}