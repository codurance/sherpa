using System.ComponentModel.DataAnnotations;

namespace SherpaFrontEnd.Dtos;

public class CreateSurveyDto
{

    [Required]
    public Guid SurveyId { get; set; }
    
    [Required(ErrorMessage = "This field is mandatory")]
    public Guid? TeamId { get; set; }
    
    [Required(ErrorMessage = "This field is mandatory")]
    public string TemplateName { get; set; }
    
    [Required(ErrorMessage = "This field is mandatory")]
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    public CreateSurveyDto()
    {
    }
    
    public CreateSurveyDto(Guid SurveyId ,Guid TeamId, string TemplateName, string Title, string? Description, DateTime? Deadline)
    {
        this.SurveyId = SurveyId;
        this.TeamId = TeamId;
        this.TemplateName = TemplateName;
        this.Title = Title;
        this.Description = Description;
        this.Deadline = Deadline;
    }
    
}