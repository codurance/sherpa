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

        // TODO: find out how this language will be configured
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
            var responseNumber = index + 1;
            csvWriter.WriteField(responseNumber);
            var response = surveyResponses[index];

            foreach (var orderedResponse in response.OrderedResponses())
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