using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Template.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.Builders;

public class TemplateBuilder
{
    private string _name = "Hackman Model";
    private IEnumerable<IQuestion> _questions = new List<IQuestion>();
    private int _minutesToComplete = 30;

    public static TemplateBuilder ATemplate()
    {
        return new TemplateBuilder();
    }

    public TemplateBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TemplateBuilder WithQuestions(IEnumerable<IQuestion> questions)
    {
        _questions = questions;
        return this;
    }

    public TemplateBuilder WithMinutesToComplete(int minutesToComplete)
    {
        _minutesToComplete = minutesToComplete;
        return this;
    }

    public Template.Domain.Template Build()
    {
        return new Template.Domain.Template(_name, _questions, _minutesToComplete);
    }

    public TemplateWithoutQuestions BuildWithoutQuestions()
    {
        return new TemplateWithoutQuestions(_name, _minutesToComplete);
    }
}