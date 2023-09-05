using MongoDB.Bson.Serialization.Attributes;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Repositories.Mongo;

public class MSurvey
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    public User Coach { get; set; }
    public Status Status { get; set; }
    public DateTime? Deadline { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<Response> Responses { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Team { get; set; }
    public string Template { get; set; }

    public MSurvey(Guid id, User coach, Status status, DateTime? deadline, string title, string? description,
        List<Response> responses, Guid team, string template)
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

    public static MSurvey FromSurvey(Survey survey)
    {
        return new MSurvey(
            survey.Id,
            survey.Coach,
            survey.Status,
            survey.Deadline,
            survey.Title,
            survey.Description,
            survey.Responses,
            survey.Team.Id,
            survey.Template.Name
        );
    }

    public static Survey ToSurvey(MSurvey mSurvey, Team surveyTeam, Template surveyTemplate)
    {
        return new Survey(
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