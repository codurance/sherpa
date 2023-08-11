using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Model;
using SherpaBackEnd.Repositories;
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

    private ISurveyService _surveysService;
    private DeprecatedSurveysController _deprecatedSurveysController;

    public SurveysControllerAcceptanceTest()
    {
        _surveyRepository = new InMemorySurveyRepository();
        _assessmentRepository = new InMemoryAssessmentRepository();
        _emailService = new Mock<IEmailService>();
        _asseptanceService = new AssessmentService(_surveyRepository, _assessmentRepository, _emailService.Object);
        _assessmentsController = new AssessmentsController(_asseptanceService);

        _surveysService = new SurveyService(_surveyRepository);
        _deprecatedSurveysController = new DeprecatedSurveysController(_surveysService);
    }

    [Fact]
    public async Task GetListOfQuestionsForSelectedTemplate()
    {
        var templatesRequest = await _assessmentsController.GetTemplatesAsync();
        var templatesResult = Assert.IsType<OkObjectResult>(templatesRequest.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);
        
        var actualQuestionsRequest = await _deprecatedSurveysController.GetQuestions(actualTemplates.First().Id);
        var actualQuestionsResult = Assert.IsType<OkObjectResult>(actualQuestionsRequest.Result);
        var actualQuestions = Assert.IsAssignableFrom<List<QuestionContent>>(actualQuestionsResult.Value);

        Assert.NotEmpty(actualQuestions);
        
    }
}