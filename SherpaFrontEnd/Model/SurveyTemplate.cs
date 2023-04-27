namespace SherpaFrontEnd.Model;

public class SurveyTemplate
{
    public string Name { get; set; }

    public Guid Id { get; set; }

    public SurveyTemplate(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}