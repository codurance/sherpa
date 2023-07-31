namespace SherpaBackEnd.Model.Template;

public class Template
{
    private Guid _id;
    private string _name;
    private int _minutesToComplete;
    private Question[] _questions;

    public Template(Guid id, string name, Question[] questions, int minutesToComplete)
    {
        _id = id;
        _name = name;
        _questions = questions;
        _minutesToComplete = minutesToComplete;
    }
}