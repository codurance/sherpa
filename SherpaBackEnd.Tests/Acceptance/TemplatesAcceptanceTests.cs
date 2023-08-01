using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class TemplatesAcceptanceTests : IDisposable
{
    private readonly Template _hackmanTemplate;
    private readonly ILogger<TemplateController> _logger;
    private const string TestFolder = "test/acceptance";
    private const string QuestionInSpanish = "Question in spanish";
    private const string QuestionInEnglish = "Question in english";
    private const string Response1 = "1";
    private const string Response2 = "2";
    private const string Response3 = "3";
    private const int Position = 1;
    private const bool Reverse = false;

    public TemplatesAcceptanceTests()
    {
        Directory.CreateDirectory(TestFolder);
        var contents =
            $@"position,responses,question_english,question_spanish,reverse,component,subcategory,subcomponent
{Position},{Response1} | {Response2} | {Response3},{QuestionInEnglish},{QuestionInSpanish},{Reverse.ToString()},{HackmanComponent.INTERPERSONAL_PEER_COACHING},{HackmanSubcategory.DELIMITED},{HackmanSubcomponent.SENSE_OF_URGENCY}
";
        File.WriteAllText($"{TestFolder}/hackman.csv", contents);
        
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
        _hackmanTemplate = new Template("Hackman Model", questions, 30);
        _logger = Mock.Of<ILogger<TemplateController>>();
    }

    [Fact]
    public async Task controller_returns_templates_list_with_hackman_template_inside()
    {
        // GIVEN a frontend that uses the template controller
        
        ITemplateRepository templateRepository = new InMemoryFilesTemplateRepository(TestFolder);
        var templateService = new TemplateService(templateRepository);
        var templateController = new TemplateController(templateService, _logger);

        // WHEN get all templates endpoint is requested
        var actualResponse = await templateController.GetAllTemplates();

        // THEN it should receive a list containing the hackman template
        var templatesResult = Assert.IsType<OkObjectResult>(actualResponse.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<Template>>(templatesResult.Value);
        
        Assert.Equal(
            JsonConvert.SerializeObject(new[]{
                _hackmanTemplate
            })
            , JsonConvert.SerializeObject(actualTemplates));
    }
    
    [Fact]
    public async Task controller_returns_status_code_500_if_there_is_an_error()
    {
        // GIVEN a frontend that uses the template controller

        ITemplateRepository templateRepository = new InMemoryFilesTemplateRepository("folder_that_doesnt_exist");
        var templateService = new TemplateService(templateRepository);
        var templateController = new TemplateController(templateService, _logger);

        // WHEN get all templates endpoint is requested but there is an error
        var actualResponse = await templateController.GetAllTemplates();

        var templatesResult = Assert.IsType<StatusCodeResult>(actualResponse.Result);
        // THEN it should receive a 500 status code
        Assert.Equal(StatusCodes.Status500InternalServerError, templatesResult.StatusCode);
    }

    public void Dispose()
    {
        File.Delete($"{TestFolder}/hackman.csv");
        Directory.Delete(TestFolder);
    }
}