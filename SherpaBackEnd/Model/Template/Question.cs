namespace SherpaBackEnd.Model.Template;

public class Question
{
    public readonly Dictionary<string, string> Statement;
    

    public Question(Dictionary<string,string> statement)
    {
        Statement = statement;
    }
}