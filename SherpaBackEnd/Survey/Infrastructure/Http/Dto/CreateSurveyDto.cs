namespace SherpaBackEnd.Survey.Infrastructure.Http.Dto;

public class CreateSurveyDto
{
    public Guid SurveyId { get; set; }
    public Guid TeamId { get; set; }
    public string TemplateName { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    
    public CreateSurveyDto(Guid surveyId ,Guid teamId, string templateName, string title, string? description, DateTime? deadline)
    {
        SurveyId = surveyId;
        TeamId = teamId;
        TemplateName = templateName;
        Title = title;
        Description = description;
        Deadline = deadline;
    }

}