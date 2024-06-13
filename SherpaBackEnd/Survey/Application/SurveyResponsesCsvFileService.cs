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

        csvWriter.WriteField("Response");
        foreach (var description in headerDescriptions)
        {
            csvWriter.WriteField(description);
        }
        csvWriter.NextRecord();
        var surveyResponses = survey.Responses;
        for (var index = 0; index < surveyResponses.Count; index++)
        {
            csvWriter.WriteField(index + 1);
            var response = surveyResponses[index];
            var orderedResponses = response.Answers.OrderBy(res => res.Number).ToList();

            foreach (var orderedResponse in orderedResponses)
            {
                csvWriter.WriteField(orderedResponse.Answer);
            }
            csvWriter.NextRecord();
        }

        csvWriter.Flush();
        
        return stream;
    }
}