using System.Threading.Tasks;
using FileHelpers;
using SherpaBackEnd.Exceptions;

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

    private void ParseTemplateFile(string templateName, IFileHelperEngine<CsvHackmanQuestion> engine,
        ICollection<Template> allTemplates)
    {
        var fileName = $"{_folder}/{_templatesFileName[templateName]}";

        try
        {
            var records = engine.ReadFile(fileName);
            var questions = new List<IQuestion>();
            foreach (var record in records)
            {
                ParseRecordAndAddToQuestions(questions, record);
            }

            allTemplates.Add(new Template(templateName, questions.ToArray(), _templatesDuration[templateName]));
        }
        catch (Exception e)
        {
            throw new CSVParsingException("Error while parsing the .csv files", e);
        }
    }

    private static void ParseRecordAndAddToQuestions(ICollection<IQuestion> questions,
        CsvHackmanQuestion csvHackmanQuestion)
    {
        questions.Add(new HackmanQuestion(new Dictionary<string, string>
            {
                { Languages.SPANISH, csvHackmanQuestion.QuestionSpanish },
                { Languages.ENGLISH, csvHackmanQuestion.QuestionEnglish }
            }, new Dictionary<string, string[]>
            {
                {
                    Languages.SPANISH, csvHackmanQuestion
                        .ResponsesSpanish.Split(" // ")
                },
                {
                    Languages.ENGLISH, csvHackmanQuestion
                        .ResponsesEnglish.Split(" // ")
                }
            }, csvHackmanQuestion.Reverse, csvHackmanQuestion.Component,
            csvHackmanQuestion.Subcategory,
            string.IsNullOrEmpty(csvHackmanQuestion.Subcomponent) ? null : csvHackmanQuestion.Subcomponent,
            csvHackmanQuestion.Position));
    }
}

[DelimitedRecord("|")]
[IgnoreFirst]
public class CsvHackmanQuestion
{
    public int Position { get; set; }
    public string ResponsesEnglish { get; set; }
    public string ResponsesSpanish { get; set; }
    public string QuestionEnglish { get; set; }
    public string QuestionSpanish { get; set; }
    public bool Reverse { get; set; }
    public string Component { get; set; }
    public string Subcategory { get; set; }
    public string? Subcomponent { get; set; }

    public CsvHackmanQuestion()
    {
    }
}