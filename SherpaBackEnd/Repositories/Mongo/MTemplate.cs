using MongoDB.Bson;
using Newtonsoft.Json;
using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Repositories.Mongo;

public class MTemplate
{
    public static Template ParseTemplate(string templateName, BsonDocument template)
    {
        return templateName switch
        {
            "Hackman Model" => HackmanModelFromRawTemplate(template),
            _ => throw new NotImplementedException()
        };
    }

    private static Template HackmanModelFromRawTemplate(BsonDocument rawTemplate)
    {
        var rawQuestions = rawTemplate.GetElement("questions").Value.AsBsonArray;

        var hackmanQuestions =
            rawQuestions.Select(value => JsonConvert.DeserializeObject<HackmanQuestion>(value.ToJson()));

        return new Template(
            "Hackman Model",
            hackmanQuestions.ToArray(),
            rawTemplate.GetElement("minutesToComplete").Value.AsInt32
        );
    }
}