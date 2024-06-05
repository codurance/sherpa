namespace SherpaBackEnd.Survey.Domain.Exceptions;

public class SurveyAlreadyAnsweredException: Exception
{
    public SurveyAlreadyAnsweredException(string? message) : base(message)
    {
    }

    public SurveyAlreadyAnsweredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}