using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class SurveysController
{
    private ISurveysService _surveysService;
    public SurveysController(ISurveysService surveysService)
    {
        _surveysService = surveysService;
    }

    public async Task<ActionResult<List<QuestionContent>>> GetQuestions(Guid templateId)
    {
        throw new NotImplementedException();
    }
}