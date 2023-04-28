namespace SherpaBackEnd.Model;

public class Survey
{
    public DateOnly Date { get; set; }
    
    public List<string> Emails { get; set; }

    public Survey(DateOnly date, List<string> emails)
    {
        Date = date;
        Emails = emails;
    }
}