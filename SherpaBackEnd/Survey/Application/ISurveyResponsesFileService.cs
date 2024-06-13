using Microsoft.AspNetCore.Mvc;

namespace SherpaBackEnd.Survey.Application;

public interface ISurveyResponsesFileService
{
    FileResult CreateFile(Domain.Survey survey);
}