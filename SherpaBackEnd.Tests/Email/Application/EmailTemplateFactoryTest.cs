using JetBrains.Annotations;
using SherpaBackEnd.Email;
using SherpaBackEnd.Email.Application;
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
        var emailTemplateFactory = new EmailTemplateFactory();
        var jane = ATeamMember().WithFullName("Jane Doe").WithEmail("jane.doe@codurance.com").Build();
        var surveyId = Guid.NewGuid();
        var survey = ASurvey().WithId(surveyId).Build();
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
        
        // TODO change url to real one
        var baseAnswerSurveyUrl = "sherpa.com/answer-survey/";
        var expectedJaneUrl = baseAnswerSurveyUrl + janeSurveyNotificationId; 
        var expectedJohnUrl = baseAnswerSurveyUrl + johnSurveyNotificationId; 
        
        var expectedEmailTemplates = new List<EmailTemplate>()
        {
            new EmailTemplate(jane.Email, expectedJaneUrl),
            new EmailTemplate(john.Email, expectedJohnUrl)
        };

        var actualEmailTemplates = emailTemplateFactory.CreateEmailTemplate(surveyNotifications);

        Assert.Equal(expectedEmailTemplates, actualEmailTemplates);
    }
}