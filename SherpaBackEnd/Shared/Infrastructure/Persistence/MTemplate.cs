using MongoDB.Bson;
using Newtonsoft.Json;
using SherpaBackEnd.Template.Domain;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MTemplate
{
    public static Template.Domain.Template ParseTemplate(string templateName, BsonDocument template)
    {
        return templateName switch
        {
            "Hackman Model" => HackmanModelFromRawTemplate(template),
            _ => throw new NotImplementedException()
        };
    }

    private static Template.Domain.Template HackmanModelFromRawTemplate(BsonDocument rawTemplate)
    {
        var rawQuestions = rawTemplate.GetElement("questions").Value.AsBsonArray;

        var hackmanQuestions =
            rawQuestions.Select(value => JsonConvert.DeserializeObject<HackmanQuestion>(value.ToJson()));

        return new Template.Domain.Template(
            "Hackman Model",
            hackmanQuestions.ToArray(),
            rawTemplate.GetElement("minutesToComplete").Value.AsInt32
        );
    }
}