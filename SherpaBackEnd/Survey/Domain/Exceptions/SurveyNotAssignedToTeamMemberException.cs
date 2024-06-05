namespace SherpaBackEnd.Survey.Domain.Exceptions;

public class SurveyNotAssignedToTeamMemberException : Exception
{
    public SurveyNotAssignedToTeamMemberException(Guid teamMemberId) : base($"{teamMemberId} is not assigned to survey")
    {
    }
}