namespace SherpaBackEnd.Model;

public class SurveyTemplate
{
    public string Name { get; set; }

    public SurveyTemplate(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; }
}