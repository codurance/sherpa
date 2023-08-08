namespace SherpaFrontEnd.Model;

public class DeprecatedSurvey
{
    public DateOnly Date { get; set; }
    
    public List<string> Emails { get; set; }
    
    public int MembersCount { get; set; }

    public DeprecatedSurvey(DateOnly date, List<string> emails)
    {
        Date = date;
        Emails = emails;
        MembersCount = emails.Count;
    }
}