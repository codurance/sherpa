using System.Data.Common;

namespace SherpaBackEnd.Exceptions;

public class RepositoryException : DbException
{
    public RepositoryException(string message) : base(message)
    {
    }
    
    public RepositoryException(string message, Exception innerException): base (message, innerException) {}
}