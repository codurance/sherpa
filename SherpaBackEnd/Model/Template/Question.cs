namespace SherpaBackEnd.Model.Template;

public class Question
{
    private readonly Dictionary<string, string> _statement;
    

    public Question(Dictionary<string,string> statement)
    {
        _statement = statement;
    }
}