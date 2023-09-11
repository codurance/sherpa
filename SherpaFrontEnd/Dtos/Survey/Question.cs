namespace SherpaFrontEnd.Dtos.Survey;

public class Question : IQuestion
{
    public Question(Dictionary<string, string> statement, Dictionary<string, string[]> responses, bool reverse, string component, string subcategory, string? subcomponent, int position)
    {
        Statement = statement;
        Responses = responses;
        Reverse = reverse;
        Component = component;
        Subcategory = subcategory;
        Subcomponent = subcomponent;
        Position = position;
    }

    public Dictionary<string, string> Statement { get; set;}
    public Dictionary<string, string[]> Responses { get; }
    public bool Reverse { get; }
    public string Component { get; }
    public string Subcategory { get; }
    public string? Subcomponent { get; }
    public int Position { get; }
}