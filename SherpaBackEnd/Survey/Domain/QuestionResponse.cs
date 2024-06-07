namespace SherpaBackEnd.Survey.Domain;

public class QuestionResponse
{
    public int Number { get; set; }
    public string Answer { get; set; }

    public QuestionResponse( int number, string answer)
    {
        Answer = answer;
        Number = number;
    }
}