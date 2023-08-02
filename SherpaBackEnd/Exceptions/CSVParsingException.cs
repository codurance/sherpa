namespace SherpaBackEnd.Exceptions;

public class CSVParsingException: Exception
{
    public CSVParsingException()
    {
    }

    public CSVParsingException(string message)
        : base(message)
    {
    }

    public CSVParsingException(string message, Exception inner)
        : base(message, inner)
    {
    }
}