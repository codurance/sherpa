using MongoDB.Bson.Serialization.Attributes;
using SherpaBackEnd.Team.Domain;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MSurveyNotification
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Survey { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid TeamMember { get; set; }

    private MSurveyNotification(Guid id, Guid surveyId, Guid teamMemberId)
    {
        Id = id;
        Survey = surveyId;
        TeamMember = teamMemberId;
    }


    public static MSurveyNotification FromSurveyNotification(
        SurveyNotification.Domain.SurveyNotification surveyNotification)
    {
        return new MSurveyNotification(
            surveyNotification.Id,
            surveyNotification.Survey.Id,
            surveyNotification.TeamMember.Id
        );
    }

    public static SurveyNotification.Domain.SurveyNotification ToSurveyNotification(
        MSurveyNotification mSurveyNotification, Survey.Domain.Survey survey, TeamMember teamMember)
    {
        return new SurveyNotification.Domain.SurveyNotification(mSurveyNotification.Id, survey, teamMember);
    }
}