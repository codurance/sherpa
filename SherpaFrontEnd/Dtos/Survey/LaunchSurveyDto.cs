namespace SherpaFrontEnd.Dtos.Survey;

public class LaunchSurveyDto
{
    public Guid SurveyId { get; }

    public LaunchSurveyDto(Guid surveyId)
    {
        SurveyId = surveyId;
    }
}