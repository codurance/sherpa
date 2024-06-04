using JetBrains.Annotations;
using Moq;
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

    public SurveyNotificationServiceTest()
    {
        _surveyRepository = new Mock<ISurveyRepository>();
        _surveyNotificationsRepository = new Mock<ISurveyNotificationsRepository>();
        _surveyNotificationService = new SurveyNotificationService(_surveyRepository.Object, _surveyNotificationsRepository.Object);
    }

    [Fact]
    public async Task ShouldRetrieveTheSurveyWithTheSurveyIdInTheDto()
    {
        await _surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);
        
        _surveyRepository.Verify(repository => repository.GetSurveyById(_createSurveyNotificationsDto.SurveyId));
    }

    [Fact]
    public async Task ShouldCreateSurveyNotificationForEachTeamMember()
    {
        var jane = ATeamMember().WithFullName("Jane Doe").Build();
        var teamMembers = new List<TeamMember>(){jane};
        var team = ATeam().WithTeamMembers(teamMembers).Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        var surveyNotificationId = Guid.NewGuid();
        var surveyNotificationService =
            new TestableSurveyNotificationService(_surveyRepository.Object, _surveyNotificationsRepository.Object)
                {
                    SurveyNotificationId = surveyNotificationId
                };
        var surveyNotification = new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, _surveyId, jane.Id);
        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId))
            .ReturnsAsync(survey);
        
        await surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);

        _surveyNotificationsRepository.Verify(repository => repository.CreateSurveyNotification(surveyNotification));

    }

    class TestableSurveyNotificationService :  SurveyNotificationService
    {
        public Guid SurveyNotificationId { get; set; }
        public TestableSurveyNotificationService(ISurveyRepository surveyRepository, ISurveyNotificationsRepository surveyNotificationsRepository) : base(surveyRepository, surveyNotificationsRepository)
        {
        }

        protected override Guid GenerateId()
        {
            return SurveyNotificationId;
        }
    }
}