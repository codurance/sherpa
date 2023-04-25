using SherpaBackEnd.Model;

namespace SherpaBackEnd.Controllers;

public class Assessment
{
    public Guid GroupId;
    public Guid TemplateId;
    public IEnumerable<Survey> Surveys;
}