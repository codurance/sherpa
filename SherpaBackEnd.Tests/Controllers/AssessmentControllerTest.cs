using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class AssessmentControllerTest
{
    private readonly AssessmentController _controller;
    private Mock<ISurveyService> mockService;

    public AssessmentControllerTest()
    {
        mockService = new();
        _controller = new AssessmentController(mockService.Object);
    }

    [Fact]
    public async Task GetTemplates_ReturnListOfTemplates_OkExpected()
    {
        mockService.Setup(service => service.GetTemplates())
            .ReturnsAsync(new List<SurveyTemplate>
            {
                new("hackman")
            });
        
        var templatesRequest = await _controller.GetTemplates();
        var templatesResult = Assert.IsType<OkObjectResult>(templatesRequest.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);
        Assert.NotEmpty(actualTemplates);
    }

    [Fact]
    public async Task GetTemplates_ReturnEmptyList_NotFoundExpected()
    {
        mockService.Setup(service => service.GetTemplates())
            .ReturnsAsync(new List<SurveyTemplate>());
        
        var templatesRequest = await _controller.GetTemplates();
        Assert.IsType<NotFoundResult>(templatesRequest.Result);
    }
}