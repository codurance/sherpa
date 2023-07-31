namespace SherpaFrontEnd.Services;

public class TemplateWithNameAndTime
{
    public string Name {get; set;}
    public int MinutesToComplete {get; set;}


    public TemplateWithNameAndTime(string name, int minutesToComplete)
    {
        Name = name;
        MinutesToComplete = minutesToComplete;
    }
}