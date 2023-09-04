using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;

namespace SherpaBackEnd.Repositories.Mongo;

public class MSurvey
{
    public Guid Id {get; set;}
    public User Coach {get; set;}
    public Status Status {get; set;}
    public DateTime? Deadline {get; set;}
    public string Title {get; set;}
    public string? Description {get; set;}
    public Response[] Responses {get; set;}
    public Guid Team {get; set;}
    public string Template {get; set;}

    public MSurvey(Guid id, User coach, Status status, DateTime? deadline, string title, string? description, Response [] responses, Guid team, string template)
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