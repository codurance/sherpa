using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Survey.Infrastructure.Http.Dto;

public class AnswerSurveyDto
{
    public AnswerSurveyDto(Guid surveyId, Guid teamMemberId, List<SurveyResponse> responses)
    {
    }
}