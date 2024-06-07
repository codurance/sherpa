using MongoDB.Bson.Serialization.Attributes;
using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MSurveyResponse
{
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid TeamMemberId { get; set; }

    public List<QuestionResponse> Answers { get; set; }

    public MSurveyResponse(Guid teamMemberId, List<QuestionResponse> answers)
    {
        TeamMemberId = teamMemberId;
        Answers = answers;
    }

    public static MSurveyResponse FromSurveyResponse(SurveyResponse surveyResponse)
    {
        return new MSurveyResponse(surveyResponse.TeamMemberId, surveyResponse.Answers);
    }

    public static SurveyResponse ToSurveyResponse(MSurveyResponse mSurveyResponse)
    {
        return new SurveyResponse(mSurveyResponse.TeamMemberId, mSurveyResponse.Answers);
    }
}