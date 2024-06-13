using Microsoft.AspNetCore.Mvc;

namespace SherpaBackEnd.Survey.Application;

public interface ISurveyResponsesFileService
{
    Stream CreateFileStream(Domain.Survey survey);
}