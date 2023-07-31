using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class TemplatesAcceptanceTests : IDisposable
{
    private const string TestFolder = "test/acceptance";

    public TemplatesAcceptanceTests()
    {
        Directory.CreateDirectory(TestFolder);
        File.WriteAllText($"{TestFolder}/hackman.csv",
            $@"position,responses,question_english,question_spanish,reverse,component,subcategory,subcomponent
1,1 | 2 | 3,Question in english,Question in spanish,false,{HackmanComponent.INTERPERSONAL_PEER_COACHING},{HackmanSubcategory.DELIMITED},{HackmanSubcomponent.SENSE_OF_URGENCY}
");
    }

    [Fact]
    public async Task controller_returns_templates_list_with_hackman_inside()
    {
        // GIVEN a frontend that uses the template controller
        var questions = new Question[]
        {
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Question in spanish" },
                    { Languages.ENGLISH, "Question in english" },
                }, new string[] { "1", "2", "3" }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING,
                HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, 1)
        };
        var hackmanTemplate = new Template("hackman", questions, 30);

        ITemplateRepository templateRepository = new InMemoryFilesTemplateRepository(TestFolder);
        var templateService = new TemplateService(templateRepository);
        var templateController = new TemplateController(templateService);

        // WHEN get all templates endpoint is requested
        var actualResponse = await templateController.GetAllTemplates();

        // THEN it should receive a list containing the hackman template
        var templatesResult = Assert.IsType<OkObjectResult>(actualResponse.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<Template>>(templatesResult.Value);
        
        Assert.Equal(
            JsonConvert.SerializeObject(new[]{
                hackmanTemplate
            })
            , JsonConvert.SerializeObject(actualTemplates));
    }

    public void Dispose()
    {
        File.Delete($"{TestFolder}/hackman.csv");
        Directory.Delete(TestFolder);
    }
}