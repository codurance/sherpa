namespace SherpaBackEnd.Template.Domain;

public class Template
{
    public string Name { get; }
    public int MinutesToComplete { get; }
    public IEnumerable<IQuestion> Questions { get; }

    public Template(string name, IEnumerable<IQuestion> questions, int minutesToComplete)
    {
        Name = name;
        Questions = questions;
        MinutesToComplete = minutesToComplete;
    }
}