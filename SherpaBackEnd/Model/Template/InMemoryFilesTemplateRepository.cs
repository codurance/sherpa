using System.Threading.Tasks;
using FileHelpers;

namespace SherpaBackEnd.Model.Template;

public class InMemoryFilesTemplateRepository : ITemplateRepository
{
    private readonly string _folder;

    private const string HackmanModel = "Hackman Model";

    private readonly string[] _templateNames = { HackmanModel };

    private readonly Dictionary<string, string> _templatesFileName = new()
    {
        { HackmanModel, "hackman.csv" }
    };

    private readonly Dictionary<string, int> _templatesDuration = new()
    {
        { HackmanModel, 30 }
    };

    public InMemoryFilesTemplateRepository(string folder)
    {
        _folder = folder;
    }

    public async Task<Template[]> GetAllTemplates()
    {
        var allTemplates = new List<Template>();
        var engine = new FileHelperEngine<CsvHackmanQuestion>();

        foreach (var templateName in _templateNames)
        {
            ParseTemplateFile(templateName, engine, allTemplates);
        }

        return allTemplates.ToArray();
    }

    private static void ParseRecordAndAddToQuestions(ICollection<IQuestion> questions, CsvHackmanQuestion csvHackmanQuestion)
    {
        questions.Add(new HackmanQuestion(new Dictionary<string, string>
            {
                { Languages.SPANISH, csvHackmanQuestion.QuestionSpanish },
                { Languages.ENGLISH, csvHackmanQuestion.QuestionEnglish }
            }, csvHackmanQuestion.Responses.Split(" | "), csvHackmanQuestion.Reverse,
            csvHackmanQuestion.Component,
            csvHackmanQuestion.Subcategory, csvHackmanQuestion.Subcomponent, csvHackmanQuestion.Position));
    }

    private void ParseTemplateFile(string templateName, FileHelperEngine<CsvHackmanQuestion> engine, List<Template> allTemplates)
    {
        var fileName = $"{_folder}/{_templatesFileName[templateName]}";
        var records = engine.ReadFile(fileName);
        var questions = new List<IQuestion>();
        foreach (var record in records)
        {
            ParseRecordAndAddToQuestions(questions, record);
        }

        allTemplates.Add(new Template(templateName, questions.ToArray(), _templatesDuration[templateName]));
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