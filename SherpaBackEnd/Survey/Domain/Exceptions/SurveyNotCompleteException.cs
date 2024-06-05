namespace SherpaBackEnd.Survey.Domain.Exceptions;

public class SurveyNotCompleteException : Exception
{
    public SurveyNotCompleteException() : base("Survey not complete")
    {
    }
}