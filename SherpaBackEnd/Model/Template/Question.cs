using System.Text.Json;
using System.Text.Json.Serialization;

namespace SherpaBackEnd.Model.Template;

public class Question
{
    public Dictionary<string, string> Statement { get; }
    

    public Question(Dictionary<string,string> statement)
    {
        Statement = statement;
    }
}

public class QuestionConverter : JsonConverter<Question>
{
    public override Question Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement this method if you need deserialization
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Question value, JsonSerializerOptions options)
    {
        if (value is HackmanQuestion hackmanQuestion)
        {
            JsonSerializer.Serialize(writer, hackmanQuestion, options);
        }
        else
        {
            JsonSerializer.Serialize(writer, new Question(value.Statement), options);
        }
    }
}