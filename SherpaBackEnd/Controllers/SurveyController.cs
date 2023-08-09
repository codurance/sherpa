using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class SurveyController
{
    public SurveyController(SurveysService surveysService)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult> CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<Survey>> GetSurveyById(Guid surveyId)
    {
        throw new NotImplementedException();
    }
}