namespace SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;

public class CreateSurveyNotificationsDto
{
    public Guid SurveyId { get; set; }

    public CreateSurveyNotificationsDto(Guid surveyId)
    {
        SurveyId = surveyId;
    }
}