namespace SherpaFrontEnd.Dtos.Survey;

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