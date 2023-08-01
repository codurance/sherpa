using SherpaBackEnd.Model.Template;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SherpaBackEnd.Tests.Model.Template;

public class InMemoryFilesTemplateRepositoryTest : IDisposable
{
    private ITestOutputHelper output;
    private const string TestFolder = "test/unit";

    public InMemoryFilesTemplateRepositoryTest(ITestOutputHelper output)
    {
        this.output = output;
        Directory.CreateDirectory(TestFolder);
        File.WriteAllText($"{TestFolder}/hackman.csv",
            $@"position,responses,question_english,question_spanish,reverse,component,subcategory,subcomponent
1,1 | 2 | 3,Question in english,Question in spanish,false,{HackmanComponent.INTERPERSONAL_PEER_COACHING},{HackmanSubcategory.DELIMITED},{HackmanSubcomponent.SENSE_OF_URGENCY}
");
    }

    [Fact]
    public async void Should_parse_csv_files_and_return_templates_with_questions()
    {
        var questions = new IQuestion[]
        {
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Question in spanish" },
                    { Languages.ENGLISH, "Question in english" },
                }, new string[] { "1", "2", "3" }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING,
                HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, 1)
        };

        var template = new SherpaBackEnd.Model.Template.Template("Hackman Model", questions, 30);

        var templateRepository = new InMemoryFilesTemplateRepository(TestFolder);
        var actualResult = await templateRepository.GetAllTemplates();
        
        Assert.Equal(
            JsonConvert.SerializeObject(new[]{
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