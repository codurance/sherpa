namespace SherpaBackEnd.Dtos;

public record CreateSurveyDto(Guid SurveyId ,Guid TeamId, string TemplateName, string Title, string Description, DateTime Deadline)
{
    
}