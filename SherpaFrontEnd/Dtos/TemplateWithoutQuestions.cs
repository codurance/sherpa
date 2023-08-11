namespace SherpaFrontEnd.Services;

public class TemplateWithoutQuestions
{
    public string Name {get; set;}
    public int MinutesToComplete {get; set;}


    public TemplateWithoutQuestions(string name, int minutesToComplete)
    {
        Name = name;
        MinutesToComplete = minutesToComplete;
    }
}