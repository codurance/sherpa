using System.Data.Common;

namespace SherpaBackEnd.Exceptions;

public class RepositoryException : DbException
{
    public RepositoryException(string message) : base(message)
    {
    }
}