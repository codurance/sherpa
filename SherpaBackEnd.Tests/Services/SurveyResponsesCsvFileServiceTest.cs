using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Tests.Builders;

namespace SherpaBackEnd.Tests.Services;

public class SurveyResponsesCsvFileServiceTest
{
    
    
    [Fact]
    public async Task ShouldCreateFileStreamWithSurveyResponses()
    {
        IEnumerable<IQuestion> questions = new IQuestion[]
        {
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 1" },
                    { Languages.ENGLISH, "Question 1" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "1", "2", "3" } },
                    { Languages.ENGLISH, new[] { "1", "2", "3" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 1
            ),
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 2" },
                    { Languages.ENGLISH, "Question 2" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "1", "2", "3" } },
                    { Languages.ENGLISH, new[] { "1", "2", "3" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 2
            ),
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 3" },
                    { Languages.ENGLISH, "Question 3" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "Uno", "Dos", "Tres" } },
                    { Languages.ENGLISH, new[] { "One", "Two", "Three" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 3
            ),
        };
        var template = TemplateBuilder.ATemplate().WithName("Hackman Model").WithQuestions(questions)
            .WithMinutesToComplete(30).Build();
        
        var janeAnswers = new List<QuestionResponse>()
        {
            new QuestionResponse(1, "1"),
            new QuestionResponse(2, "3"),
            new QuestionResponse(3, "Two"),
        };
        var johnAnswers = new List<QuestionResponse>()
        {
            new QuestionResponse(1, "2"),
            new QuestionResponse(2, "1"),
            new QuestionResponse(3, "Three"),
        };
        var responses = new List<SurveyResponse>()
        {
            new SurveyResponse(Guid.NewGuid(), janeAnswers),
            new SurveyResponse(Guid.NewGuid(), johnAnswers)
        };
        var survey = SurveyBuilder.ASurvey().WithId(Guid.NewGuid()).WithTemplate(template).WithResponses(responses)
            .Build();
        
        var expectedCsvContent = "Response,1. Question 1,2. Question 2,3. Question 3\n1,1,3,Two\n2,2,1,Three";
        var surveyResponsesCsvFileService = new SurveyResponsesCsvFileService();
        var streamResult = surveyResponsesCsvFileService.CreateFileStream(survey);
        
        streamResult.Position = 0;
        using (var streamReader = new StreamReader(streamResult))
        {
            var content = await streamReader.ReadToEndAsync();
            Assert.Equal(expectedCsvContent, content);
        }
    }
    
}