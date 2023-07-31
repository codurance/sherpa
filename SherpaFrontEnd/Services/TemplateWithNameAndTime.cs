namespace SherpaFrontEnd.Services;

public class TemplateWithNameAndTime
{
    public string Name;
    public int MinutesToComplete;


    public TemplateWithNameAndTime(string name, int minutesToComplete)
    {
        Name = name;
        MinutesToComplete = minutesToComplete;
    }
}