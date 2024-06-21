using JetBrains.Annotations;
using Moq;
using SherpaBackEnd.Email;
using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Email.Templates;
using SherpaBackEnd.Email.Templates.NewSurvey;
using SherpaBackEnd.Exceptions;
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
    private static Guid _surveyId = Guid.NewGuid();
    private CreateSurveyNotificationsDto _createSurveyNotificationsDto = new CreateSurveyNotificationsDto(_surveyId);
    private Mock<ISurveyNotificationsRepository> _surveyNotificationsRepository;
    private Mock<IEmailTemplateFactory> _emailTemplateFactory;
    private SurveyNotificationService _surveyNotificationService;
    private Mock<IEmailService> _emailService;

    public SurveyNotificationServiceTest()
    {
        _surveyRepository = new Mock<ISurveyRepository>();
        _surveyNotificationsRepository = new Mock<ISurveyNotificationsRepository>();
        _emailTemplateFactory = new Mock<IEmailTemplateFactory>();
        _emailService = new Mock<IEmailService>();
        _surveyNotificationService = new SurveyNotificationService(_surveyRepository.Object,
            _surveyNotificationsRepository.Object,
            _emailTemplateFactory.Object, _emailService.Object);
    }

    [Fact]
    public async Task ShouldRetrieveTheSurveyWithTheSurveyIdInTheDto()
    {
        var team = ATeam().Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId)).ReturnsAsync(survey);

        await _surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);

        _surveyRepository.Verify(repository => repository.GetSurveyById(_createSurveyNotificationsDto.SurveyId));
    }


    [Fact]
    public async Task ShouldCreateSurveyNotificationForTeamMembers()
    {
        var jane = ATeamMember().WithFullName("Jane Doe").Build();
        var john = ATeamMember().WithFullName("John Doe").Build();
        var teamMembers = new List<TeamMember>() { jane, john };
        var team = ATeam().WithTeamMembers(teamMembers).Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        var surveyNotificationId = Guid.NewGuid();
        var testableSurveyNotificationService =
            new TestableSurveyNotificationService(_surveyRepository.Object, _surveyNotificationsRepository.Object,
                _emailTemplateFactory.Object, _emailService.Object)
            {
                SurveyNotificationId = surveyNotificationId
            };
        var janeSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, jane);
        var johnSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, john);
        var surveyNotifications =
            new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>()
                { janeSurveyNotification, johnSurveyNotification };

        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId))
            .ReturnsAsync(survey);

        await testableSurveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);

        _surveyNotificationsRepository.Verify(
            repository => repository.CreateManySurveyNotification(surveyNotifications));
    }

    [Fact]
    public async Task ShouldCreateEmailTemplateForSurvey()
    {
        var jane = ATeamMember().WithFullName("Jane Doe").Build();
        var john = ATeamMember().WithFullName("John Doe").Build();
        var teamMembers = new List<TeamMember>() { jane, john };
        var team = ATeam().WithTeamMembers(teamMembers).Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        var surveyNotificationId = Guid.NewGuid();
        var testableSurveyNotificationService =
            new TestableSurveyNotificationService(_surveyRepository.Object, _surveyNotificationsRepository.Object,
                _emailTemplateFactory.Object, _emailService.Object)
            {
                SurveyNotificationId = surveyNotificationId
            };
        var janeSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, jane);
        var johnSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, john);
        var surveyNotifications =
            new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>()
                { janeSurveyNotification, johnSurveyNotification };
        var newSurveyEmailTemplateDto = new NewSurveyEmailTemplateDto(surveyNotifications);

        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId))
            .ReturnsAsync(survey);

        await testableSurveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);


        _emailTemplateFactory.Verify(factory => factory.CreateEmailTemplate(It.IsAny<CreateEmailTemplateDto>()));
        _emailTemplateFactory.Verify(factory =>
            factory.CreateEmailTemplate(It.Is<NewSurveyEmailTemplateDto>(
                dto => dto.SurveyNotifications.Contains(johnSurveyNotification) &&
                       dto.SurveyNotifications.Contains(janeSurveyNotification))));
    }

    [Fact]
    public async Task ShouldSendEmailWithTemplateRequests()
    {
        var jane = ATeamMember().WithFullName("Jane Doe").Build();
        var teamMembers = new List<TeamMember>() { jane };
        var team = ATeam().WithTeamMembers(teamMembers).Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        var surveyNotificationId = Guid.NewGuid();
        var testableSurveyNotificationService =
            new TestableSurveyNotificationService(_surveyRepository.Object, _surveyNotificationsRepository.Object,
                _emailTemplateFactory.Object, _emailService.Object)
            {
                SurveyNotificationId = surveyNotificationId
            };
        var janeSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, jane);
        var surveyNotifications =
            new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>()
                { janeSurveyNotification };

        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId))
            .ReturnsAsync(survey);
        var emailTemplate = new EmailTemplate(null, null, new List<Recipient>());
        var newSurveyEmailTemplateDto = new NewSurveyEmailTemplateDto(surveyNotifications);
        _emailTemplateFactory.Setup(factory => factory.CreateEmailTemplate(It.IsAny<NewSurveyEmailTemplateDto>()))
            .Returns(emailTemplate);

        await testableSurveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto);

        _emailService.Verify(service => service.SendEmailsWith(emailTemplate));
    }

    [Fact]
    public async Task ShouldThrowErrorIfGetSurveyByIdIsNotSuccessful()
    {
        _surveyRepository.Setup(repository =>
            repository.GetSurveyById(_createSurveyNotificationsDto.SurveyId)).ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () =>
            await _surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto));

        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }

    [Fact]
    public async Task ShouldThrowErrorIfGetSurveyByIdIsNotFound()
    {
        _surveyRepository.Setup(repository =>
            repository.GetSurveyById(_createSurveyNotificationsDto.SurveyId)).ReturnsAsync((Survey.Domain.Survey?)null);

        var exceptionThrown = await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto));
        Assert.IsType<NotFoundException>(exceptionThrown);
    }

    [Fact]
    public async Task ShouldThrowErrorIfCreateManySurveyNotificationIsNotSuccessful()
    {
        var surveyNotifications = new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>();
        var team = ATeam().Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId)).ReturnsAsync(survey);

        _surveyNotificationsRepository
            .Setup(repository => repository.CreateManySurveyNotification(surveyNotifications))
            .ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () =>
            await _surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto));
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }

    [Fact]
    public async Task ShouldGetSurveyNotificationFromRepository()
    {
        var teamMember = ATeamMember().Build();
        var survey = ASurvey().Build();
        var surveyNotificationId = Guid.NewGuid();
        var expectedSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(surveyNotificationId, survey, teamMember);

        _surveyNotificationsRepository.Setup(repository => repository.GetSurveyNotificationById(surveyNotificationId))
            .ReturnsAsync(expectedSurveyNotification);

        var surveyNotification = await _surveyNotificationService.GetSurveyNotification(surveyNotificationId);

        Assert.Equal(expectedSurveyNotification, surveyNotification);
    }


    [Fact]
    public async Task ShouldThrowErrorIfSendEmailIsNotSuccessful()
    {
        var surveyNotifications = new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>();
        var team = ATeam().Build();
        var survey = ASurvey().WithId(_surveyId).WithTeam(team).Build();
        _surveyRepository.Setup(repository => repository.GetSurveyById(_surveyId)).ReturnsAsync(survey);
        var emailTemplate = new EmailTemplate(null, null, new List<Recipient>());
        _emailTemplateFactory.Setup(factory => factory.CreateEmailTemplate(It.IsAny<NewSurveyEmailTemplateDto>()))
            .Returns(emailTemplate);

        _emailService
            .Setup(service => service.SendEmailsWith(emailTemplate))
            .ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<EmailSendingException>(async () =>
            await _surveyNotificationService.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto));
        Assert.IsType<EmailSendingException>(exceptionThrown);
    }

    // TODO remove TestableSurveyNotificationService by implementing IGuidService mock in tests
    class TestableSurveyNotificationService : SurveyNotificationService
    {
        public Guid SurveyNotificationId { get; set; }

        public TestableSurveyNotificationService(ISurveyRepository surveyRepository,
            ISurveyNotificationsRepository surveyNotificationsRepository,
            IEmailTemplateFactory emailTemplateFactory, IEmailService emailService) : base(surveyRepository,
            surveyNotificationsRepository,
            emailTemplateFactory, emailService)
        {
        }

        protected override Guid GenerateId()
        {
            return SurveyNotificationId;
        }
    }
}