using Moq;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;
using SherpaFrontEnd.Services.Email;

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

    [Fact]
    public async Task testEmailServiceIsCalledIfAssessmentIsUpdated()
    {
        Assessment assessment = new Assessment(Guid.NewGuid(), Guid.NewGuid(), "assessment")
        {
            Surveys = new List<Survey>
            {
                new Survey(DateOnly.FromDateTime(DateTime.Today), new List<string> { "email 1", "email 2" })
            }
        };
        _assessmentRepository.Setup(r => r.UpdateAssessment(It.IsAny<Dtos.Assessment>()))
            .ReturnsAsync(assessment);

        await _assessmentService.UpdateAssessment(assessment);
        
        _emailService.Verify(m => m.sendEmail(It.IsAny<string>(),It.Is<List<String>>(email => email.Count.Equals(2))));
    }
}