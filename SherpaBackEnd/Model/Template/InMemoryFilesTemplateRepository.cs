using FileHelpers;

namespace SherpaBackEnd.Model.Template;

public class InMemoryFilesTemplateRepository : ITemplateRepository
{
    private readonly string _folder;

    private readonly string[] _templates = new[] { "hackman" };

    private readonly Dictionary<string, string> _templatesFileName = new()
    {
        { "hackman", "hackman.csv" }
    };

    private readonly Dictionary<string, int> _templatesDuration = new()
    {
        { "hackman", 30 }
    };

    public InMemoryFilesTemplateRepository(string folder)
    {
        _folder = folder;
    }

    public async Task<Template[]> GetAllTemplates()
    {
        var allTemplates = new List<Template>();

        foreach (var template in _templates)
        {
            var engine = new FileHelperEngine<CsvHackmanQuestion>();
            var fileName = $"{_folder}/{_templatesFileName[template]}";
            var records = engine.ReadFile(fileName);
            allTemplates.Add(new Template(template, records.Select(csvHackmanQuestion => new HackmanQuestion(new() { { Languages.SPANISH, csvHackmanQuestion.QuestionSpanish }, { Languages.ENGLISH, csvHackmanQuestion.QuestionEnglish } }, csvHackmanQuestion.Responses.Split(" | "), csvHackmanQuestion.Reverse, csvHackmanQuestion.Component, csvHackmanQuestion.Subcategory, csvHackmanQuestion.Subcomponent, csvHackmanQuestion.Position)).Cast<Question>().ToArray(), _templatesDuration[template]));
        }

        return allTemplates.ToArray();
    }
}

[DelimitedRecord(",")]
[IgnoreFirst]
public class CsvHackmanQuestion
{
    public int Position { get; set; }
    public string Responses { get; set; }
    public string QuestionEnglish { get; set; }
    public string QuestionSpanish { get; set; }
    public bool Reverse { get; set; }
    public string Component { get; set; }
    public string Subcategory { get; set; }
    public string Subcomponent { get; set; }

    public CsvHackmanQuestion()
    {
    }
}