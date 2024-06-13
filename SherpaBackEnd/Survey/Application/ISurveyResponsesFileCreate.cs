using Microsoft.AspNetCore.Mvc;

namespace SherpaBackEnd.Survey.Application;

public interface ISurveyResponsesFileCreate
{
    FileResult CreateFile(Domain.Survey survey);
}