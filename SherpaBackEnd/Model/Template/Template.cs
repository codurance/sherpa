namespace SherpaBackEnd.Model.Template;

public class Template
{
    public string Name{get;}
    public int MinutesToComplete{get;}
    public IQuestion[] Questions{get;}

    public Template(string name, IQuestion[] questions, int minutesToComplete)
    {
        Name = name;
        Questions = questions;
        MinutesToComplete = minutesToComplete;
    }
}