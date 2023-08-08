using SherpaBackEnd.Model;

namespace SherpaBackEnd.Dtos;

public class Assessment
{
    public Guid TeamId { get; }
    public Guid TemplateId { get; }
    
    public string Name { get; }
    public IEnumerable<DeprecatedSurvey> Surveys{ get; set; }
    
    public Assessment(Guid teamId, Guid templateId, string name)
    {
        TeamId = teamId;
        TemplateId = templateId;
        Name = name;
        Surveys = new List<DeprecatedSurvey>();
    }

    public List<string> GetLastSurveyEmails()
    {
        return Surveys.Last().Emails;
    }
    
}