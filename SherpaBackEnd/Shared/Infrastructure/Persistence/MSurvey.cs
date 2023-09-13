using MongoDB.Bson.Serialization.Attributes;
using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MSurvey
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    public User.Domain.User Coach { get; set; }
    public SurveyStatus Status { get; set; }
    public DateTime? Deadline { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<SurveyResponse> Responses { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Team { get; set; }
    public string Template { get; set; }

    public MSurvey(Guid id, User.Domain.User coach, SurveyStatus status, DateTime? deadline, string title, string? description,
        List<SurveyResponse> responses, Guid team, string template)
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

    public static MSurvey FromSurvey(Survey.Domain.Survey survey)
    {
        return new MSurvey(
            survey.Id,
            survey.Coach,
            survey.SurveyStatus,
            survey.Deadline,
            survey.Title,
            survey.Description,
            survey.Responses,
            survey.Team.Id,
            survey.Template.Name
        );
    }

    public static Survey.Domain.Survey ToSurvey(MSurvey mSurvey, Team.Domain.Team surveyTeam, Template.Domain.Template surveyTemplate)
    {
        return new Survey.Domain.Survey(
            mSurvey.Id,
            mSurvey.Coach,
            mSurvey.Status,
            mSurvey.Deadline,
            mSurvey.Title,
            mSurvey.Description,
            mSurvey.Responses,
            surveyTeam,
            surveyTemplate
        );
    }
}