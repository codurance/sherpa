namespace SherpaBackEnd.Model.Template;

// TODO: Can this be an interface?
public class Question
{
    public Dictionary<string, string> Statement { get; }
    

    public Question(Dictionary<string,string> statement)
    {
        Statement = statement;
    }
}