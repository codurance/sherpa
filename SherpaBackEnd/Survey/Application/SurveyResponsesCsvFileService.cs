using System.Globalization;
using CsvHelper;
using SherpaBackEnd.Survey.Domain;
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
        WriteHeader(csvWriter, headerDescriptions);

        var surveyResponses = survey.Responses;
        WriteBody(csvWriter, surveyResponses);

        csvWriter.Flush();

        return stream;
    }

    private void WriteBody(CsvWriter csvWriter, List<SurveyResponse> surveyResponses)
    {
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
    }

    private void WriteHeader(CsvWriter csvWriter, List<string> headerFields)
    {
        csvWriter.WriteField("Response");
        foreach (var description in headerFields)
        {
            csvWriter.WriteField(description);
        }

        csvWriter.NextRecord();
    }
}