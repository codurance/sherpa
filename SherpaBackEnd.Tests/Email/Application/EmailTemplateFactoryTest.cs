using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Moq;
using SherpaBackEnd.Email;
using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Email.Templates.NewSurvey;
using static SherpaBackEnd.Tests.Builders.SurveyBuilder;
using static SherpaBackEnd.Tests.Builders.TeamBuilder;
using static SherpaBackEnd.Tests.Builders.TeamMemberBuilder;

namespace SherpaBackEnd.Tests.Email.Application;

[TestSubject(typeof(EmailTemplateFactory))]
public class EmailTemplateFactoryTest
{

    [Fact]
    public void ShouldCreateEmailTemplatesFromSurveyNotifications()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();

        var httpScheme = "http";
        var sherpaUrl = new HostString("www.sherpa.com");
        httpContextAccessor.SetupProperty(context => context.HttpContext.Request.Scheme, httpScheme);
        httpContextAccessor.SetupProperty(context => context.HttpContext.Request.Host, sherpaUrl);
        
        var emailTemplateFactory = new EmailTemplateFactory(httpContextAccessor.Object);
        var jane = ATeamMember().WithFullName("Jane Doe").WithEmail("jane.doe@codurance.com").Build();
        var surveyId = Guid.NewGuid();
        var survey = ASurvey().WithId(surveyId).WithDescription("Answer this").Build();
        var janeSurveyNotificationId = Guid.NewGuid();
        var janeSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(janeSurveyNotificationId, survey, jane);
        var john = ATeamMember().WithFullName("John Doe").WithEmail("john.doe@codurance.com").Build();
        var johnSurveyNotificationId = Guid.NewGuid();
        var johnSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(johnSurveyNotificationId, survey, john);
        var surveyNotifications = 
            new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>()
            {
                janeSurveyNotification,
                johnSurveyNotification
            };
        
        var baseAnswerSurveyUrl = httpScheme + "://" + sherpaUrl + "/answer-survey/";
        var expectedJaneUrl = baseAnswerSurveyUrl + janeSurveyNotificationId; 
        var expectedJohnUrl = baseAnswerSurveyUrl + johnSurveyNotificationId;
        var janeRecipient = new Recipient(jane.FullName,jane.Email, expectedJaneUrl);
        var johnRecipient = new Recipient(john.FullName, john.Email, expectedJohnUrl);
        List<Recipient> recipients = new List<Recipient>()
        {
            janeRecipient,
            johnRecipient
        };
        
        var expectedEmailTemplates = new EmailTemplate("NewSurvey", recipients);
        var newSurveyEmailTemplateDto = new NewSurveyEmailTemplateDto(surveyNotifications);

        var actualEmailTemplates = emailTemplateFactory.CreateEmailTemplate(newSurveyEmailTemplateDto);

        Assert.Equal(expectedEmailTemplates.TemplateName, actualEmailTemplates.TemplateName);
        Assert.Contains(janeRecipient, actualEmailTemplates.Recipients);
        Assert.Contains(johnRecipient, actualEmailTemplates.Recipients);
    }
}