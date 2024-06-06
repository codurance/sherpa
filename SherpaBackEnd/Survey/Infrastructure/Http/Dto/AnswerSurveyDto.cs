using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Survey.Infrastructure.Http.Dto;

public class AnswerSurveyDto
{
    public Guid SurveyId { get; set; }
    public Guid TeamMemberId { get; set; }
    public SurveyResponse Response { get; set; }

    public AnswerSurveyDto(Guid surveyId, Guid teamMemberId, SurveyResponse response)
    {
        SurveyId = surveyId;
        TeamMemberId = teamMemberId;
        Response = response;
    }
}