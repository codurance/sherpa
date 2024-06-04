using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Survey.Infrastructure.Http.Dto;

public class AnswerSurveyDto
{
    public readonly Guid SurveyId;
    public readonly Guid TeamMemberId;
    public readonly List<SurveyResponse> Responses;

    public AnswerSurveyDto(Guid surveyId, Guid teamMemberId, List<SurveyResponse> responses)
    {
        SurveyId = surveyId;
        TeamMemberId = teamMemberId;
        Responses = responses;
    }
}