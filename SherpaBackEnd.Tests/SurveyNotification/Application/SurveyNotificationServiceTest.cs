using JetBrains.Annotations;
using Moq;
using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using static SherpaBackEnd.Tests.Builders.SurveyBuilder;
using static SherpaBackEnd.Tests.Builders.TeamBuilder;
using static SherpaBackEnd.Tests.Builders.TeamMemberBuilder;

namespace SherpaBackEnd.Tests.SurveyNotification.Application;

[TestSubject(typeof(SurveyNotificationService))]
public class SurveyNotificationServiceTest
{
    private Mock<ISurveyRepository> _surveyRepository;
    private SurveyNotificationService _surveyNotificationService;
    private CreateSurveyNotificationsDto _createSurveyNotificationsDto = new CreateSurveyNotificationsDto(_surveyId);
    private static Guid _surveyId = Guid.NewGuid();
    private Mock<ISurveyNotificationsRepository> _surveyNotificationsRepository;
    private Mock<EmailTemplateFactory> _emailTemplateFactory;

    public SurveyNotificationServiceTest()
    {
        _surveyRepository = new Mock<ISurveyRepository>();
        _surveyNotificationsRepository = new Mock<ISurveyNotificationsRepository>();
        _emailTemplateFactory = new Mock<EmailTemplateFactory>();
        _surveyNotificationService = new SurveyNotificationService(_surveyRepository.Object, _surveyNotificationsRepository.Object, _emailTemplateFactory.Object);
    }

    [Fact]
    public async Task ShouldRetrieveTheSurveyWithTheSurveyIdInTheDto()
    {
        await _surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);
        
        _surveyRepository.Verify(repository => repository.GetSurveyById(_createSurveyNotificationsDto.SurveyId));
    }
    
    [Fact]
    public async Task ShouldCreateSurveyNotificationForTeamMembers()
    {
        var jane = ATeamMember().WithFullName("Jane Doe").Build();
        var john = ATeamMember().WithFullName("John Doe").Build();
        var teamMembers = new List<TeamMember>(){jane, john};
        var team = ATeam().WithTeamMembers(teamMembers).Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        var surveyNotificationId = Guid.NewGuid();
        var surveyNotificationService =
            new TestableSurveyNotificationService(_surveyRepository.Object, _surveyNotificationsRepository.Object, null)
                {
                    SurveyNotificationId = surveyNotificationId
                };
        var janeSurveyNotification = new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, jane);
        var johnSurveyNotification = new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, john);
        var surveyNotifications =
            new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>(){janeSurveyNotification, johnSurveyNotification};
        
        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId))
            .ReturnsAsync(survey);
        
        await surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);

        _surveyNotificationsRepository.Verify(repository => repository.CreateManySurveyNotification(surveyNotifications));
    }
    
    [Fact]
    public async Task ShouldCreateEmailTemplateForSurvey()
    {
        var jane = ATeamMember().WithFullName("Jane Doe").Build();
        var john = ATeamMember().WithFullName("John Doe").Build();
        var teamMembers = new List<TeamMember>(){jane, john};
        var team = ATeam().WithTeamMembers(teamMembers).Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        var surveyNotificationId = Guid.NewGuid();
        var surveyNotificationService =
            new TestableSurveyNotificationService(_surveyRepository.Object, _surveyNotificationsRepository.Object, _emailTemplateFactory.Object)
            {
                SurveyNotificationId = surveyNotificationId
            };
        var janeSurveyNotification = new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, jane);
        var johnSurveyNotification = new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, john);
        var surveyNotifications =
            new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>(){janeSurveyNotification, johnSurveyNotification};
        
        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId))
            .ReturnsAsync(survey);
        
        await _surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);

        _emailTemplateFactory.Verify(factory => factory.CreateEmailTemplate(surveyNotifications));
    }
    

    class TestableSurveyNotificationService :  SurveyNotificationService
    {
        public Guid SurveyNotificationId { get; set; }
        public TestableSurveyNotificationService(ISurveyRepository surveyRepository, ISurveyNotificationsRepository surveyNotificationsRepository, EmailTemplateFactory emailTemplateFactory) : base(surveyRepository, surveyNotificationsRepository, emailTemplateFactory)
        {
        }

        protected override Guid GenerateId()
        {
            return SurveyNotificationId;
        }
    }
}