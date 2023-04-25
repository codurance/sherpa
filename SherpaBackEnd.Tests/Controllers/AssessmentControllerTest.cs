﻿using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class AssessmentControllerTest
{
    private readonly AssessmentController _controller;
    private Mock<ISurveyService> _mockService;

    public AssessmentControllerTest()
    {
        _mockService = new();
        _controller = new AssessmentController(_mockService.Object);
    }

    [Fact]
    public async Task GetTemplates_ReturnListOfTemplates_OkExpected()
    {
        _mockService.Setup(service => service.GetTemplates())
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
        _mockService.Setup(service => service.GetTemplates())
            .ReturnsAsync(new List<SurveyTemplate>());
        
        var templatesRequest = await _controller.GetTemplates();
        Assert.IsType<NotFoundResult>(templatesRequest.Result);
    }

    [Fact]
    public async Task AddAssessment_ReturnNewAssessment_OkResultExpected()
    {
        var groupId = Guid.NewGuid();
        var templateId = Guid.NewGuid();

        _mockService.Setup(m => m.IsTemplateExist(templateId)).ReturnsAsync(true);
        var assessmentRequest = await _controller.AddAssessment(groupId, templateId);
        var assessmentResult = Assert.IsType<OkObjectResult>(assessmentRequest.Result);
        var actualAssessment = Assert.IsAssignableFrom<Assessment>(assessmentResult.Value);
        
        Assert.Equal(groupId, actualAssessment.GroupId);
        Assert.Equal(templateId, actualAssessment.TemplateId);
        Assert.Empty(actualAssessment.Surveys);
        
    }
    
    [Fact]
    public async Task AddAssessmentWithNonExistingTemplate_BadRequestExpected()
    {
        _mockService.Setup(m => m.GetTemplates()).ReturnsAsync(new List<SurveyTemplate>
        {
            new ("Template A")
        });
        
        var assessmentRequest = await _controller.AddAssessment(Guid.NewGuid(), Guid.Empty);
        Assert.IsType<BadRequestResult>(assessmentRequest.Result);
        
    }
}