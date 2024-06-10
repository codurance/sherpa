namespace SherpaBackEnd.SurveyNotification.Infrastructure.Http.Responses;

public class SurveyNotificationResponse
{
    private Guid Id { get; set; }
    private Guid SurveyId { get; set; }
    private Guid TeamMemberId { get; set; }

    public static SurveyNotificationResponse FromSurveyNotification(Domain.SurveyNotification surveyNotification)
    {
        return new SurveyNotificationResponse()
        {
            Id = surveyNotification.Id,
            SurveyId = surveyNotification.Survey.Id,
            TeamMemberId = surveyNotification.TeamMember.Id
        };
    }
}