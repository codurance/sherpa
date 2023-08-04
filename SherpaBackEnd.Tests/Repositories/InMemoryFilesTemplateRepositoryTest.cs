using Newtonsoft.Json;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Repositories;

namespace SherpaBackEnd.Tests.Repositories;

public class InMemoryFilesTemplateRepositoryTest : IDisposable
{
    private const string TestFolder = "test/unit";
    private const string QuestionInSpanish = "Question in spanish";
    private const string QuestionInEnglish = "Question in english";
    private const string ResponseSpanish1 = "SPA_1";
    private const string ResponseSpanish2 = "SPA_2";
    private const string ResponseSpanish3 = "SPA_3";
    private const string ResponseEnglish1 = "ENG_1";
    private const string ResponseEnglish2 = "ENG_2";
    private const string ResponseEnglish3 = "ENG_3";
    private const int Position = 1;
    private const bool Reverse = false;

    public InMemoryFilesTemplateRepositoryTest()
    {
        Directory.CreateDirectory(TestFolder);
        var contents =
                $@"position|responses_english|responses_spanish|question_english|question_spanish|reverse|component|subcategory|subcomponent
{Position}|{ResponseEnglish1} // {ResponseEnglish2} // {ResponseEnglish3}|{ResponseSpanish1} // {ResponseSpanish2} // {ResponseSpanish3}|{QuestionInEnglish}|{QuestionInSpanish}|{Reverse.ToString()}|{HackmanComponent.INTERPERSONAL_PEER_COACHING}|{HackmanSubcategory.DELIMITED}|{HackmanSubcomponent.SENSE_OF_URGENCY}
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
                }, new Dictionary<string, string[]>()
                {
                    {
                        Languages.SPANISH, new[] { ResponseSpanish1, ResponseSpanish2, ResponseSpanish3 }
                    },
                    {
                        Languages.ENGLISH, new[] { ResponseEnglish1, ResponseEnglish2, ResponseEnglish3 }
                    }
                }, Reverse,
                HackmanComponent.INTERPERSONAL_PEER_COACHING,
                HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, Position)
        };

        var template = new Template("Hackman Model", questions, 30);

        var templateRepository = new InMemoryFilesTemplateRepository(TestFolder);
        var actualResult = await templateRepository.GetAllTemplatesAsync();

        Assert.Equal(
            JsonConvert.SerializeObject(new[]
            {
                template
            })
            , JsonConvert.SerializeObject(actualResult));
    }
    
    [Fact]
    public async Task Should_throw_a_CSVParsingException_if_there_is_any_error_parsing_the_csv_file()
    {
        var templateRepository = new InMemoryFilesTemplateRepository("folder_that_does_not_exist");

        var csvParsingException = await Assert.ThrowsAnyAsync<CSVParsingException>(async () => await templateRepository.GetAllTemplatesAsync());
        
        Assert.Equal("Error while parsing the .csv files", csvParsingException.Message);
    }

    public void Dispose()
    {
        File.Delete($"{TestFolder}/hackman.csv");
        Directory.Delete(TestFolder);
    }
}