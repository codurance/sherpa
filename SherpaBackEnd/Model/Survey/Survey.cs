using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model.Survey;

public class Survey
{
    public Guid Id;
    public User Coach;
    public Status Status;
    public DateTime Deadline;
    public string Title;
    public string Description;
    public Response[] Responses;
    public Team Team;
    public Template.Template Template;

    public Survey(Guid id, User coach, Status status, DateTime deadline, string title, string description, Response [] responses, Team team, Template.Template template)
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