using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class DeprecatedSurveysController
{
    private ISurveyService _surveysService;
    public DeprecatedSurveysController(ISurveyService surveysService)
    {
        _surveysService = surveysService;
    }

    public async Task<ActionResult<List<QuestionContent>>> GetQuestions(Guid templateId)
    {
        var questions = new List<QuestionContent>
        {
            new QuestionContent()
        };
        return new OkObjectResult(questions);
    }
}