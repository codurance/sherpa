namespace SherpaBackEnd.Model.Template;

public class Template
{
    public string Name;
    public int MinutesToComplete;
    public Question[] Questions;

    public Template(string name, Question[] questions, int minutesToComplete)
    {
        Name = name;
        Questions = questions;
        MinutesToComplete = minutesToComplete;
    }
}