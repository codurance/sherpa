namespace SherpaBackEnd.Model;

public class DatabaseSettings
{
    public string DatabaseName { get; set; } = null!;

    public string TeamsCollectionName { get; set; } = null!;
    
    public string TeamMembersCollectionName { get; set; } = null!;
    
    public string? ConnectionString { get; set; } = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    public string TemplateCollectionName { get; set; } = null!;
    public string SurveyCollectionName { get; set; } = null!;
}