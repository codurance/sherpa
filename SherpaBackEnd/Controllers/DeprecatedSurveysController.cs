using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class DeprecatedSurveysController
{
    private ISurveysService _surveysService;
    public DeprecatedSurveysController(ISurveysService surveysService)
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