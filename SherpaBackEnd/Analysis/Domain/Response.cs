using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Analysis.Domain;

public class Response<T>
{
    public string Category { get; }
    public string Value { get; }
    public bool Reverse { get; }
    public List<T> Options { get; }
    
    public Response(string category, string value, bool reverse, List<T> options)
    {
        Category = category;
        Value = value;
        Reverse = reverse;
        Options = options;
    }
}