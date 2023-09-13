using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Template.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Survey.Infrastructure.Http.Dto;

public class SurveyWithoutQuestions
{
    public Guid Id {get; set;}
    public User.Domain.User Coach {get; set;}
    public SurveyStatus SurveyStatus {get; set;}
    public DateTime? Deadline {get; set;}
    public string Title {get; set;}
    public string? Description {get; set;}
    public List<SurveyResponse> Responses {get; set;}
    public Team.Domain.Team Team {get; set;}
    public TemplateWithoutQuestions Template {get; set;}
    

    public SurveyWithoutQuestions(Guid id, User.Domain.User coach, SurveyStatus surveyStatus, DateTime? deadline, string title, string? description, List<SurveyResponse> responses, Team.Domain.Team team, TemplateWithoutQuestions template)
    {
        Id = id;
        Coach = coach;
        SurveyStatus = surveyStatus;
        Deadline = deadline;
        Title = title;
        Description = description;
        Responses = responses;
        Team = team;
        Template = template;
    }
}