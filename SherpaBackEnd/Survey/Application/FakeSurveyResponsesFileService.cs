using System.Text;

namespace SherpaBackEnd.Survey.Application;

public class FakeSurveyResponsesFileService : ISurveyResponsesFileService
{
    public Stream CreateFileStream(Domain.Survey survey)
    {
        var dummyCsvContent = "Id,Response\n1,Yes\n2,No";
        var dummyCsvBytes = Encoding.UTF8.GetBytes(dummyCsvContent);
        var surveyResponsesFileStream = new MemoryStream(dummyCsvBytes);
        return surveyResponsesFileStream;
    }
}