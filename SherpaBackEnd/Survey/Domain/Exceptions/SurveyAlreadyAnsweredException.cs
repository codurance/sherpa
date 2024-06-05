namespace SherpaBackEnd.Survey.Domain.Exceptions;

public class SurveyAlreadyAnsweredException: Exception
{
    public SurveyAlreadyAnsweredException(Guid teamMemberId) : base($"Survey already answered by {teamMemberId}")
    {
    }
}