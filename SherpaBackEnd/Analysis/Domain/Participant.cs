namespace SherpaBackEnd.Analysis.Domain;

public class Participant<T>
{
    public List<Response<T>> Responses { get; }
    
    public Participant(List<Response<T>> responses)
    {
        Responses = responses;
    }

}