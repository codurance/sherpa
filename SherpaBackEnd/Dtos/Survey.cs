namespace SherpaBackEnd.Model;

public class Survey
{
    public DateOnly Date;
    
    public List<string> Emails { get; set; }

    public int Completed = 0;

    public Survey(DateOnly date, List<string> emails)
    {
        Date = date;
        Emails = emails;
    }
}