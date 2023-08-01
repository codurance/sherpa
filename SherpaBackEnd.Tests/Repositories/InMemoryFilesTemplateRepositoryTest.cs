using Newtonsoft.Json;
using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Tests.Repositories;

public class InMemoryFilesTemplateRepositoryTest : IDisposable
{
    private const string TestFolder = "test/unit";
    private const string QuestionInSpanish = "Question in spanish";
    private const string QuestionInEnglish = "Question in english";
    private const string Response1 = "1";
    private const string Response2 = "2";
    private const string Response3 = "3";
    private const int Position = 1;
    private const bool Reverse = false;

    public InMemoryFilesTemplateRepositoryTest()
    {
        Directory.CreateDirectory(TestFolder);
        var contents =
            $@"position,responses,question_english,question_spanish,reverse,component,subcategory,subcomponent
{Position},{Response1} | {Response2} | {Response3},{QuestionInEnglish},{QuestionInSpanish},{Reverse.ToString()},{HackmanComponent.INTERPERSONAL_PEER_COACHING},{HackmanSubcategory.DELIMITED},{HackmanSubcomponent.SENSE_OF_URGENCY}
";
        File.WriteAllText($"{TestFolder}/hackman.csv", contents);
    }

    [Fact]
    public async Task Should_parse_csv_files_and_return_templates_with_questions()
    {
        var questions = new IQuestion[]
        {
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, QuestionInSpanish },
                    { Languages.ENGLISH, QuestionInEnglish },
                }, new string[] { Response1, Response2, Response3 }, Reverse,
                HackmanComponent.INTERPERSONAL_PEER_COACHING,
                HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, Position)
        };

        var template = new SherpaBackEnd.Model.Template.Template("Hackman Model", questions, 30);

        var templateRepository = new InMemoryFilesTemplateRepository(TestFolder);
        var actualResult = await templateRepository.GetAllTemplates();

        Assert.Equal(
            JsonConvert.SerializeObject(new[]
            {
                template
            })
            , JsonConvert.SerializeObject(actualResult));
    }

    public void Dispose()
    {
        File.Delete($"{TestFolder}/hackman.csv");
        Directory.Delete(TestFolder);
    }
}