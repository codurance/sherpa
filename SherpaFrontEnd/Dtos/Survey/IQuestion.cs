namespace SherpaFrontEnd.Dtos.Survey;

public interface IQuestion
{
    public Dictionary<string, string> Statement { get; }
    public Dictionary<string, string[]> Responses{get;}
    public bool Reverse{get;}
    public string Component{get;}
    public string Subcategory{get;}
    public string? Subcomponent{get;}
    public int Position{get;}
}