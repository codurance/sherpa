namespace SherpaBackEnd.Model.Template;

public class Template
{
    public string Name{get;}
    public int MinutesToComplete{get;}
    public Question[] Questions{get;}

    public Template(string name, Question[] questions, int minutesToComplete)
    {
        Name = name;
        Questions = questions;
        MinutesToComplete = minutesToComplete;
    }
}