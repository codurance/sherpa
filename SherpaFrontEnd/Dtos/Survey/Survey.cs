namespace SherpaFrontEnd.Dtos.Survey;

public class Survey
{
    public Guid Id { get; set; }
    public User Coach { get; set; }
    public Status Status { get; set; }
    public DateTime? Deadline { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Response[] Responses { get; set; }
    public Team.Team Team { get; set; }
    public Template Template { get; set; }

    public Survey(Guid id, User coach, Status status, DateTime? deadline, string title, string? description, Response [] responses, Team.Team team, Template template)
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