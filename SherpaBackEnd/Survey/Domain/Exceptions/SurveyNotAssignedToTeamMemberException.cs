namespace SherpaBackEnd.Survey.Domain.Exceptions;

public class SurveyNotAssignedToTeamMemberException: Exception
{
    public SurveyNotAssignedToTeamMemberException(string? message) : base(message)
    {
    }

    public SurveyNotAssignedToTeamMemberException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}