using Moq;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;
using SherpaBackEnd.Services.Email;

namespace SherpaBackEnd.Tests.Services;

public class AssessmentServiceTest
{
    private Mock<ISurveyRepository> _surveyRepository;
    private Mock<IAssessmentRepository> _assessmentRepository;
    private Mock<IEmailService> _emailService;

    private AssessmentService _assessmentService;

    public AssessmentServiceTest()
    {
        _surveyRepository = new Mock<ISurveyRepository>();
        _assessmentRepository = new Mock<IAssessmentRepository>();
        _emailService = new Mock<IEmailService>();

        _assessmentService = new AssessmentService(_surveyRepository.Object, _assessmentRepository.Object, _emailService.Object);
    }

    // [Fact]
    // public async Task testEmailServiceIsCalledIfAssessmentIsUpdated()
    // {
    //     Assessment assessment = new Assessment(Guid.NewGuid(), Guid.NewGuid(), "assessment")
    //     {
    //         Surveys = new List<Survey>
    //         {
    //             new Survey(DateOnly.FromDateTime(DateTime.Today), new List<string> { "email 1", "email 2" })
    //         }
    //     };
    //     _assessmentRepository.Setup(r => r.UpdateAssessment(It.IsAny<Dtos.Assessment>()))
    //         .ReturnsAsync(assessment);
    //
    //     await _assessmentService.UpdateAssessment(assessment);
    //     
    //     _emailService.Verify(m => m.sendEmail(It.IsAny<string>(),It.Is<List<String>>(email => email.Count.Equals(2))));
    // }
    
    
    [Fact]
    public async Task AddSurveyForGroupWithMembers()
    {
        var group = new Group("Group");
        group.Members = new List<GroupMember>
        {
            new ("Name A", "LastName A", "Position A", "emaila@mail.com"),
            new ("Name B", "LastName B", "Position B", "emailb@mail.com")
        };
        var assessment = new Assessment(group.Id, Guid.NewGuid(), "assessment");

        _assessmentRepository.Setup(m => m.GetAssessment(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(assessment);
        
        var emails = group.Members.Select(m => m.Email).ToList();
        var survey = new Survey(DateOnly.FromDateTime(DateTime.Now), emails);
        var surveysList = new List<Survey> { survey };
        assessment.Surveys = surveysList;

        _assessmentRepository.Setup(m => m.UpdateAssessment(assessment)).ReturnsAsync(assessment);
            
        var updatedAssessment = await _assessmentService.UpdateAssessment(assessment);
        
        Assert.Equal(assessment.GroupId, updatedAssessment.GroupId);
        Assert.Equal(assessment.TemplateId, updatedAssessment.TemplateId);
        Assert.Equal(assessment.Name, updatedAssessment.Name);
        Assert.NotEmpty(updatedAssessment.Surveys);

        var addedSurvey = updatedAssessment.Surveys.First();
        Assert.Equal(group.Members.Count(), addedSurvey.MembersCount);
        Assert.Equal(group.Members.First().Email, addedSurvey.Emails.First());
        Assert.Equal(group.Members.Last().Email, addedSurvey.Emails.Last());
    }

    [Fact]
    public async Task DeleteSurveyFromExistingAssessment_UpdateAssessmentWasInvoked()
    {
        var group = new Group("Group");
        var assessment = new Assessment(group.Id, Guid.NewGuid(), "assessment");

        assessment.Surveys = new List<Survey>
        {
            new (DateOnly.FromDateTime(DateTime.Now), new List<string>())
        };
        
        _assessmentRepository.Setup(m => m.GetAssessment(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(assessment);

        assessment.Surveys = new List<Survey>();
        await _assessmentService.UpdateAssessment(assessment);
        _assessmentRepository.Verify(m => m.UpdateAssessment(assessment));
    }
}