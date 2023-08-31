namespace SherpaBackEnd.Model;

public class DatabaseSettings
{
    public string DatabaseName { get; set; } = null!;

    public string TeamsCollectionName { get; set; } = null!;
    
    public string TeamMembersCollectionName { get; set; } = null!;
}