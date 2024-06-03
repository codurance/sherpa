namespace SherpaBackEnd.Sur;

public class LaunchSurveyDto
{
    public Guid SurveyId { get; set; }

    public LaunchSurveyDto(Guid surveyId)
    {
        SurveyId = surveyId;
    }
}