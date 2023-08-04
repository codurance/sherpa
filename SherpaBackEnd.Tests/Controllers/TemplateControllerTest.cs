using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class TemplateControllerTest
{
    private readonly Mock<ITemplateService> _templateService;
    private readonly TemplateController _templateController;

    public TemplateControllerTest()
    {
        _templateService = new Mock<ITemplateService>();
        var logger = Mock.Of<ILogger<TemplateController>>();
        _templateController = new TemplateController(_templateService.Object, logger);
    }

    [Fact]
    public async Task Should_return_templates_returned_by_the_service()
    {
        var template = new Template("test", Array.Empty<IQuestion>(), 10);
        var arrayWithTemplate = new[] { template };
        
        _templateService.Setup(service => service.GetAllTemplatesAsync()).ReturnsAsync(arrayWithTemplate);

        var templatesRequest = await _templateController.GetAllTemplatesAsync();
        var templatesResult = Assert.IsType<OkObjectResult>(templatesRequest.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<Template>>(templatesResult.Value);
        Assert.Equal(arrayWithTemplate, actualTemplates);
    }

    [Fact]
    public async Task Should_return_status_code_500_if_some_error_is_thrown()
    {
        _templateService.Setup(service => service.GetAllTemplatesAsync()).ThrowsAsync(new Exception());
    
        var templatesRequest = await _templateController.GetAllTemplatesAsync();
    
        var templatesResult = Assert.IsType<StatusCodeResult>(templatesRequest.Result);
        Assert.Equal(500, templatesResult.StatusCode);
    }
}