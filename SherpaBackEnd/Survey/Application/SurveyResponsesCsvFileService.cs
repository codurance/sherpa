using System.Globalization;
using CsvHelper;
using SherpaBackEnd.Template.Domain;

namespace SherpaBackEnd.Survey.Application;

public class SurveyResponsesCsvFileService : ISurveyResponsesFileService
{
    public Stream CreateFileStream(Domain.Survey survey)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

        var headerDescriptions = survey.Template.OrderedQuestions().Select(question =>
            $"{question.GetPosition()}. {question.Statement[Languages.ENGLISH]}").ToList();

        foreach (var description in headerDescriptions)
        {
            csvWriter.WriteField(description);
        }
        
        return stream;
    }
}