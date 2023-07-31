namespace SherpaBackEnd.Model.Template;

public class Question
{
    public Dictionary<string, string> Statement { get; }
    

    public Question(Dictionary<string,string> statement)
    {
        Statement = statement;
    }
}