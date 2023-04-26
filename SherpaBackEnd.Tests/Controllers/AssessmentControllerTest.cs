﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class AssessmentControllerTest
{
    private readonly AssessmentsController _controller;
    private Mock<ISurveyService> _mockService;

    public AssessmentControllerTest()
    {
        _mockService = new();
        _controller = new AssessmentsController(_mockService.Object);
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
        var assessment = new Assessment(Guid.NewGuid(), Guid.NewGuid(), "Assessment A");
        
        _mockService.Setup(m => m.AddAssessment(assessment.GroupId, assessment.TemplateId, assessment.Name))
            .Returns(new Assessment(assessment.GroupId, assessment.TemplateId, assessment.Name));

        var assessmentRequest = await _controller.AddAssessment(assessment);
        var assessmentResult = Assert.IsType<OkObjectResult>(assessmentRequest.Result);
        var actualAssessment = Assert.IsAssignableFrom<Assessment>(assessmentResult.Value);
        
        Assert.Equal(assessment.GroupId, actualAssessment.GroupId);
        Assert.Equal(assessment.TemplateId, actualAssessment.TemplateId);
        Assert.Equal(assessment.Name, actualAssessment.Name);
        Assert.Empty(actualAssessment.Surveys);
    }
    
    [Fact]
    public async Task AddAssessmentWithNonExistingTemplate_BadRequestExpected()
    {
        _mockService.Setup(m => m.GetTemplates()).ReturnsAsync(new List<SurveyTemplate>
        {
            new ("Template A")
        });
        
        var assessmentRequest = await _controller.AddAssessment(new Assessment(Guid.Empty, Guid.NewGuid(), "Assessment A"));
        Assert.IsType<BadRequestResult>(assessmentRequest.Result);
        
    }
}