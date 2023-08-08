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
    private Mock<IAssessmentService> _mockService;

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
        
        var templatesRequest = await _controller.GetTemplatesAsync();
        var templatesResult = Assert.IsType<OkObjectResult>(templatesRequest.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);
        Assert.NotEmpty(actualTemplates);
    }

    [Fact]
    public async Task GetTemplates_ReturnEmptyList_NotFoundExpected()
    {
        _mockService.Setup(service => service.GetTemplates())
            .ReturnsAsync(new List<SurveyTemplate>());
        
        var templatesRequest = await _controller.GetTemplatesAsync();
        Assert.IsType<NotFoundResult>(templatesRequest.Result);
    }

    [Fact]
    public async Task AddAssessment_ReturnNewAssessment_OkResultExpected()
    {
        var assessment = new Assessment(Guid.NewGuid(), Guid.NewGuid(), "Assessment A");
        
        _mockService.Setup(m => m.AddAssessment(assessment.TeamId, assessment.TemplateId, assessment.Name))
            .ReturnsAsync(new Assessment(assessment.TeamId, assessment.TemplateId, assessment.Name));

        var assessmentRequest = await _controller.AddAssessmentAsync(assessment);
        var assessmentResult = Assert.IsType<OkObjectResult>(assessmentRequest.Result);
        var actualAssessment = Assert.IsAssignableFrom<Assessment>(assessmentResult.Value);
        
        Assert.Equal(assessment.TeamId, actualAssessment.TeamId);
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
        
        var assessmentRequest = await _controller.AddAssessmentAsync(new Assessment(Guid.Empty, Guid.NewGuid(), "Assessment A"));
        Assert.IsType<BadRequestResult>(assessmentRequest.Result);
        
    }

    [Fact]
    public void AddSurveySendsEmailToNoneSentSurveys()
    {
        var assessment = new Assessment(Guid.NewGuid(), Guid.NewGuid(), "assessment");
        _mockService.Setup(m => m.GetAssessment(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(assessment);
    }

    [Fact]
    public async Task AddSurveyForTeamWithMembers_OkResultExpected()
    {
        var team = new Team("Team");
        team.Members = new List<TeamMember>
        {
            new (Guid.NewGuid(), "LastName A", "Position A", "emaila@mail.com"),
            new (Guid.NewGuid(), "LastName B", "Position B", "emailb@mail.com")
        };
        var assessment = new Assessment(team.Id, Guid.NewGuid(), "assessment");

        _mockService.Setup(m => m.GetAssessment(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(assessment);
        
        var emails = team.Members.Select(m => m.Email).ToList();
        var survey = new DeprecatedSurvey(DateOnly.FromDateTime(DateTime.Now), emails);
        var surveysList = new List<DeprecatedSurvey> { survey };
        assessment.Surveys = surveysList;

        _mockService.Setup(m => m.UpdateAssessment(assessment)).ReturnsAsync(assessment);
            
        var updatedAssessmentRequest = await _controller.UpdateAssessmentAsync(assessment);
        
        var updateAssessmentResult = Assert.IsType<OkObjectResult>(updatedAssessmentRequest.Result);
        var actualUpdatedAssessmentAssessment = Assert.IsAssignableFrom<Assessment>(updateAssessmentResult.Value);
        
        Assert.Equal(assessment.TeamId, actualUpdatedAssessmentAssessment.TeamId);
        Assert.Equal(assessment.TemplateId, actualUpdatedAssessmentAssessment.TemplateId);
        Assert.Equal(assessment.Name, actualUpdatedAssessmentAssessment.Name);
        Assert.NotEmpty(actualUpdatedAssessmentAssessment.Surveys);

        var addedSurvey = actualUpdatedAssessmentAssessment.Surveys.First();
        Assert.Equal(team.Members.Count(), addedSurvey.MembersCount);
    }
    
    [Fact]
    public async Task DeleteSurveyFromExistingAssessment_OkResultExpected()
    {
        var team = new Team("Team");
        var assessment = new Assessment(team.Id, Guid.NewGuid(), "assessment");

        var survey = new DeprecatedSurvey(DateOnly.FromDateTime(DateTime.Now), new List<string>());
        assessment.Surveys = new List<DeprecatedSurvey> { survey };

        _mockService.Setup(m => m.GetAssessment(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(assessment);
            
        var updatedAssessmentRequest = await _controller.UpdateAssessmentAsync(assessment);
        
        Assert.IsType<OkObjectResult>(updatedAssessmentRequest.Result);
        _mockService.Verify(m => m.UpdateAssessment(assessment));
    }
}