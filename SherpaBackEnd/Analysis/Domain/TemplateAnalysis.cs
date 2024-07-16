namespace SherpaBackEnd.Analysis.Domain;

public class TemplateAnalysis
{
    public string Name { get; }

    public Dictionary<int, Question> Questions;
    
    public TemplateAnalysis(string name, Dictionary<int, Question> questions)
    {
        Name = name;
        Questions = questions;
    }
}