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
        var template = new Template(Guid.NewGuid(), "test", Array.Empty<Question>(), 10);
        var arrayWithTemplate = new[] { template };

        var templateService = new Mock<ITemplateService>();
        templateService.Setup(service => service.GetAllTemplates()).ReturnsAsync(arrayWithTemplate);
        var templateController = new TemplateController(templateService.Object);

        var templatesRequest = await templateController.GetAllTemplates();
        var templatesResult = Assert.IsType<OkObjectResult>(templatesRequest.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<Template>>(templatesResult.Value);
        Assert.Equal(arrayWithTemplate, actualTemplates);
    }

    // [Fact]
    // public async void Should_return_status_code_500_if_some_error_is_thrown()
    // {
    //     var templateService = new Mock<ITemplateService>();
    //     templateService.Setup(service => service.GetAllTemplates()).ThrowsAsync(new Exception());
    //     var templateController = new TemplateController(templateService.Object);
    //
    //     var templatesRequest = await templateController.GetAllTemplates();
    //
    //     var templatesResult = Assert.IsType<StatusCodeResult>(templatesRequest.Result);
    //     Assert.Equal(500, templatesResult.StatusCode);
    // }
}