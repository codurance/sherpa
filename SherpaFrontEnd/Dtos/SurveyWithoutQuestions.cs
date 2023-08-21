using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Services;

namespace SherpaFrontEnd.Dtos;

public class SurveyWithoutQuestions
{
    public Guid Id {get; set;}
    public User Coach {get; set;}
    public Status Status {get; set;}
    public DateTime? Deadline {get; set;}
    public string Title {get; set;}
    public string? Description {get; set;}
    public Response[] Responses {get; set;}
    public Team Team {get; set;}
    public TemplateWithoutQuestions Template {get; set;}
    

    public SurveyWithoutQuestions(Guid id, User coach, Status status, DateTime? deadline, string title, string? description, Response[] responses, Team team, TemplateWithoutQuestions template)
    {
        Id = id;
        Coach = coach;
        Status = status;
        Deadline = deadline;
        Title = title;
        Description = description;
        Responses = responses;
        Team = team;
        Template = template;
    }
}