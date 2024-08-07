namespace SherpaBackEnd.Analysis.Domain;

public class Question
{
    public string Category { get; }
    
    public string? SubCategory { get; }
    
    public int Position { get; }

    public bool Reverse { get; }
    
    public List<string> Options { get; }
    
    public Question(string category, string? subCategory, int position, bool reverse, List<string> options)
    {
        Category = category;
        SubCategory = subCategory;
        Position = position;
        Reverse = reverse;
        Options = options;
    }

}