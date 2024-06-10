namespace SherpaBackEnd.SurveyNotification.Infrastructure.Http.Responses;

public class SurveyNotificationResponse
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid TeamMemberId { get; set; }

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