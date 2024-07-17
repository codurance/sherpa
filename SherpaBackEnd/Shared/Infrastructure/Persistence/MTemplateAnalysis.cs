using MongoDB.Bson;
using Newtonsoft.Json;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Template.Domain;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MTemplateAnalysis
{
    public static TemplateAnalysis ParseTemplate(string templateName, BsonDocument template)
    {
        return templateName switch
        {
            "Hackman Model" => HackmanAnalysisFromRawTemplate(template),
            _ => throw new NotImplementedException()
        };
    }

    private static TemplateAnalysis HackmanAnalysisFromRawTemplate(BsonDocument rawTemplate)
    {
        var rawQuestions = rawTemplate.GetElement("questions").Value.AsBsonArray;

        var hackmanQuestions =
            rawQuestions.Select(value => JsonConvert.DeserializeObject<HackmanQuestion>(value.ToJson()));

        var questions = new Dictionary<int, Question>();

        foreach (var question in hackmanQuestions)
        {
            if (question == null) continue;
            
            questions.Add(question.Position,
                new Question(question.Component, question.Subcategory, question.Position, question.Reverse,
                    new List<string>(question.Responses["ENGLISH"])));
        }

        return new TemplateAnalysis("Hackman Model", questions);
    }
}