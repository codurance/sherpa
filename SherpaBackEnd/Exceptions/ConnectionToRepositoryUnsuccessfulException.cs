using System.Data.Common;

namespace SherpaBackEnd.Exceptions;

public class ConnectionToRepositoryUnsuccessfulException : DbException
{
    public ConnectionToRepositoryUnsuccessfulException(string message) : base(message)
    {
    }
    
    public ConnectionToRepositoryUnsuccessfulException(string message, Exception innerException): base (message, innerException) {}
}