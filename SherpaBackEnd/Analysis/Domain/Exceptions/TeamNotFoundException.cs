namespace SherpaBackEnd.Analysis.Domain.Exceptions;

public class TeamNotFoundException : Exception
{
    public TeamNotFoundException(string message) : base(message)
    {
    }
}