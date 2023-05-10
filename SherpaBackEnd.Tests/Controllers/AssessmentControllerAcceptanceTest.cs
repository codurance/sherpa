using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;
using SherpaBackEnd.Services.Email;

namespace SherpaBackEnd.Tests.Controllers;

public class AssessmentControllerAcceptanceTest
{
    private readonly InMemorySurveyRepository _inMemorySurveyRepository;
    private readonly InMemoryAssessmentRepository _inMemoryAssessmentRepository;
    private readonly Mock<IEmailService> _emailService;
    private readonly AssessmentService _assessmentService;
    private readonly AssessmentsController _assessmentsController;

    public AssessmentControllerAcceptanceTest()
    {
        _inMemorySurveyRepository = new InMemorySurveyRepository();
        _inMemoryAssessmentRepository = new InMemoryAssessmentRepository();
        _emailService = new Mock<IEmailService>();
        _assessmentService = new AssessmentService(_inMemorySurveyRepository, _inMemoryAssessmentRepository,_emailService.Object);
        _assessmentsController = new AssessmentsController(_assessmentService);
    }

    [Fact]
    public async Task AddNewAssessmentWithExistingTemplate_OkResultExpected()
    {
        
        var templates = await _assessmentsController.GetTemplatesAsync();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        var actualTemplates = Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var assessment = new Assessment(Guid.NewGuid(), actualTemplates.First().Id, "Assessment A");
        var assessmentRequest = await  _assessmentsController.AddAssessmentAsync(assessment);
        
        var assessmentResult = Assert.IsType<OkObjectResult>(assessmentRequest.Result);
        var actualAssessment = Assert.IsAssignableFrom<Assessment>(assessmentResult.Value);
        Assert.Equal(assessment.GroupId, actualAssessment.GroupId);
        Assert.Equal(assessment.TemplateId, actualAssessment.TemplateId);
        Assert.Equal(assessment.Name, actualAssessment.Name);
        Assert.Empty(actualAssessment.Surveys);
    }
    
    [Fact]
    public async Task AddNewAssessmentWithNonExistingTemplate_BadRequestExpected()
    {
        
        var templates = await _assessmentsController.GetTemplatesAsync();
        var templatesResult = Assert.IsType<OkObjectResult>(templates.Result);
        Assert.IsAssignableFrom<IEnumerable<SurveyTemplate>>(templatesResult.Value);

        var assessment = await  _assessmentsController.AddAssessmentAsync(new Assessment(Guid.NewGuid(), Guid.NewGuid(), "Assessment A"));
        
        Assert.IsType<BadRequestResult>(assessment.Result);
    }

    [Fact]
    public async Task AddNewSurveyToNonExistingAssessment_NotFoundExpected()
    {
        var groupId = Guid.NewGuid();
        var templateId = Guid.NewGuid();
        var assessmentToUpdate = new Assessment(groupId, templateId, "test assessment");
        // _inMemoryAssessmentRepository.AddAssessment(assessmentToUpdate);

        var updateAssessment = await _assessmentsController.UpdateAssessmentAsync(assessmentToUpdate);

        Assert.IsType<NotFoundResult>(updateAssessment.Result);
    }
    
    [Fact]
    public async Task AddNewSurveyWithExistingAssessment_OkResultExpected()
    {
        var groupId = Guid.NewGuid();
        var templateId = Guid.NewGuid();
        var assessmentToUpdate = new Assessment(groupId, templateId, "test assessment");
        _inMemoryAssessmentRepository.AddAssessment(assessmentToUpdate);


        var survey = new Survey(DateOnly.FromDateTime(DateTime.Now), new List<string>());
        var surveysList = new List<Survey> { survey };
        assessmentToUpdate.Surveys = surveysList;

        var updateAssessmentRequest = await _assessmentsController.UpdateAssessmentAsync(assessmentToUpdate);
        
        var updatedAssessmentResult = Assert.IsType<OkObjectResult>(updateAssessmentRequest.Result);
        var actualUpdatedAssessment = Assert.IsAssignableFrom<Assessment>(updatedAssessmentResult.Value);
        
        Assert.NotEmpty(actualUpdatedAssessment.Surveys);
        Assert.Single(actualUpdatedAssessment.Surveys);
    }

    [Fact]
    public async Task AddNewAssessmentAndNewSurveyForExistingGroupWithMembers_OkResultExpected()
    {
        var group = new Group("Group");
        group.Members = new List<GroupMember>
        {
            new ("Name A", "LastName A", "Position A", "emaila@mail.com"),
            new ("Name B", "LastName B", "Position B", "emailb@mail.com")
        };

        var templateId = Guid.NewGuid();
        var assessmentToUpdate = new Assessment(group.Id, templateId, "test assessment");
        _inMemoryAssessmentRepository.AddAssessment(assessmentToUpdate);

        var emails = group.Members.Select(m => m.Email).ToList();
        
        var survey = new Survey(DateOnly.FromDateTime(DateTime.Now), emails);
        var surveysList = new List<Survey> { survey };
        assessmentToUpdate.Surveys = surveysList;

        var updateAssessmentRequest = await _assessmentsController.UpdateAssessmentAsync(assessmentToUpdate);
        var updatedAssessmentResult = Assert.IsType<OkObjectResult>(updateAssessmentRequest.Result);
        var actualUpdatedAssessment = Assert.IsAssignableFrom<Assessment>(updatedAssessmentResult.Value);
        
        Assert.NotEmpty(actualUpdatedAssessment.Surveys);
        Assert.Single(actualUpdatedAssessment.Surveys);
        Assert.Equal(group.Members.Count(), actualUpdatedAssessment.Surveys.First().MembersCount);
    }


    [Fact]
    public async Task GetAssessmentsFromSingleGroup_OkResultExpected()
    {
        
        var group = new Group("Group");
        group.Members = new List<GroupMember>
        {
            new ("Name A", "LastName A", "Position A", "emaila@mail.com"),
            new ("Name B", "LastName B", "Position B", "emailb@mail.com")
        };
        var anotherGroup = new Group("another Group");
        anotherGroup.Members = new List<GroupMember>
        {
            new ("Name C", "LastName C", "Position C", "emailc@mail.com"),
            new ("Name D", "LastName D", "Position D", "emaild@mail.com")
        };
        
        var emails = group.Members.Select(m => m.Email).ToList();
        var anotherEmails = anotherGroup.Members.Select(m => m.Email).ToList();

        var templateId = Guid.NewGuid();
        var assessmentToGet = new Assessment(group.Id, templateId, "test assessment");
        var anotherAssessmentToGet = new Assessment(anotherGroup.Id, templateId, "another test assessment");

        assessmentToGet.Surveys = new List<Survey> { new (DateOnly.FromDateTime(DateTime.Now), emails) };
        anotherAssessmentToGet.Surveys = new List<Survey> { new (DateOnly.FromDateTime(DateTime.Now), anotherEmails) };
        
        _inMemoryAssessmentRepository.AddAssessment(assessmentToGet);
        _inMemoryAssessmentRepository.AddAssessment(anotherAssessmentToGet);

        var getAssessmentsRequest = await _assessmentsController.GetAssessmentsAsync();
        var getAssessmentsResult = Assert.IsType<OkObjectResult>(getAssessmentsRequest.Result);
        var actualAssessments = Assert.IsAssignableFrom<IEnumerable<Assessment>>(getAssessmentsResult.Value);
        
        
        Assert.NotEmpty(actualAssessments);
        Assert.Equal(2, actualAssessments.Count());
        
        var getAssessmentRequest = await _assessmentsController.GetAssessmentsAsync(group.Id);
        var getAssessmentResult = Assert.IsType<OkObjectResult>(getAssessmentRequest.Result);
        var actualAssessment = Assert.IsAssignableFrom<IEnumerable<Assessment>>(getAssessmentResult.Value);


        Assert.Contains(actualAssessment, a => a.GroupId == group.Id);

        var notFoundAssessment = await _assessmentsController.GetAssessmentsAsync(Guid.Empty);
        Assert.IsType<NotFoundResult>(notFoundAssessment.Result);
    }
}