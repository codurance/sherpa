namespace SherpaFrontEnd.Dtos.Survey;

public class Question : IQuestion
{
    public Dictionary<string, string> Statement { get; }
}