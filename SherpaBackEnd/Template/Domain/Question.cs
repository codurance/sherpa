namespace SherpaBackEnd.Template.Domain;

public interface IQuestion
{
    public Dictionary<string, string> Statement { get; }
    int GetPosition();
}