using System.Text.Json;
using System.Text.Json.Serialization;
using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Controllers.Parsers;

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