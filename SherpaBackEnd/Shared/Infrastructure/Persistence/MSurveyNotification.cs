using MongoDB.Bson.Serialization.Attributes;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MSurveyNotification
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }

    public Guid Survey { get; set; }

    public Guid TeamMember { get; set; }

    private MSurveyNotification(Guid id, Guid surveyId, Guid teamMemberId)
    {
        Id = id;
        Survey = surveyId;
        TeamMember = teamMemberId;
    }


    public static MSurveyNotification FromSurvey(SurveyNotification.Domain.SurveyNotification surveyNotification)
    {
        return new MSurveyNotification(
            surveyNotification.Id,
            surveyNotification.Survey.Id,
            surveyNotification.TeamMember.Id
        );
    }
}