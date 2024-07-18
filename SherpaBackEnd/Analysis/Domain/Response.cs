using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Analysis.Domain;

public class Response<T>
{
    public string Category { get; }
    public T Value { get; }
    public bool Reverse { get; }
    public List<T> Options { get; }
    
    public Response(string category, T value, bool reverse, List<T> options)
    {
        Category = category;
        Value = value;
        Reverse = reverse;
        Options = new List<T>(options);
        if (Reverse)
        {
            Options.Reverse();
        }
    }

    public bool IsPositive()
    {
        var indexOfResponse = Options.IndexOf(Value) + 1;

        var indexOfOptionsMidPoint = Options.Count / 2 + 1;
        if (IsEvenNumberOfOptions())
        {
            indexOfOptionsMidPoint--;
        }

        return indexOfResponse > indexOfOptionsMidPoint;
    }

    private bool IsEvenNumberOfOptions()
    {
        return Options.Count % 2 == 0;
    }
}