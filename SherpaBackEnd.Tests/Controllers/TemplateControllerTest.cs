using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class TemplateControllerTest
{
    [Fact]
    public async void Should_call_service_and_return_service_returned_object()
    {
        var template = new Template(Guid.NewGuid(), "test", new Question[] { }, 10);
        var arrayWithTemplate = new []{template};
        
        var templateService = new Mock<ITemplateService>();
        templateService.Setup(service => service.GetAllTemplates()).ReturnsAsync(arrayWithTemplate);
        var templateController = new TemplateController(templateService.Object);
        
        var templatesRequest = await templateController.GetAllTemplates();
        var templatesResult = Assert.IsType<OkObjectResult>(templatesRequest.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<Template>>(templatesResult.Value);
        Assert.Equal(arrayWithTemplate, actualTemplates);
    }
}