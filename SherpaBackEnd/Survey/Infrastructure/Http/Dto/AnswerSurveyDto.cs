using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Survey.Infrastructure.Http.Dto;

public class AnswerSurveyDto
{
    public readonly Guid SurveyId;
    public readonly Guid TeamMemberId;
    public readonly SurveyResponse Response;

    public AnswerSurveyDto(Guid surveyId, Guid teamMemberId, SurveyResponse response)
    {
        SurveyId = surveyId;
        TeamMemberId = teamMemberId;
        Response = response;
    }
}