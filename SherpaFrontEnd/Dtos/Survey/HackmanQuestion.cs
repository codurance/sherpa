namespace SherpaFrontEnd.Dtos.Survey;

public class HackmanQuestion : IQuestion
{
    public Dictionary<string, string> Statement { get; }
    public Dictionary<string, string[]> Responses{get;}
    public bool Reverse{get;}
    public string Component{get;}
    public string Subcategory{get;}
    public string? Subcomponent{get;}
    public int Position{get;}


    public HackmanQuestion(Dictionary<string, string> statement, Dictionary<string, string[]> responses, bool reverse, string component, string subcategory, string? subcomponent, int position)
    {
        Statement = statement;
        Responses = responses;
        Reverse = reverse;
        Component = component;
        Subcategory = subcategory;
        Subcomponent = subcomponent;
        Position = position;
    }
}

public static class Languages
{
    public static readonly string SPANISH = "SPANISH";
    public static readonly string ENGLISH = "ENGLISH";
}

public static class HackmanComponent
{
    public const string RealTeam = "Real Team";
    public const string CompellingDirection = "Compelling Direction";
    public const string EnablingStructure = "Enabling Structure";
    public const string SupportiveOrganizationalContext = "Supportive Organizational Context";
    public const string ExpertCoaching = "Expert Coaching";
    public const string SenseOfUrgency = "Sense Of Urgency";
}

public static class HackmanSubcategory
{
    public const string Delimited = "Delimited";
    public const string Interdependent = "Interdependent";
    public const string Stable = "Stable";
    public const string Clear = "Clear";
    public const string Challenging = "Challenging";
    public const string Consecuential = "Consecuential";
    public const string EndsVsMeans = "Ends Vs Means";
    public const string TeamComposition = "Team Composition";
    public const string TeamTaskDesign = "Team Task Design";
    public const string RewardsRecognition = "Rewards/Recognition";
    public const string Information = "Information";
    public const string EducationConsultation = "Education/Consultation";
    public const string MaterialResources = "Material/Resources";
    public const string FocusOfLeadersAttention = "Focus of Leader’s Attention";
    public const string CoachingAvailability = "Coaching Availability";
    public const string HelfulnessOfTeamLeaderCoaching = "Helpfulness of team leader coaching";
    public const string ExtentAndFocusOfCoachingProvidedByPeers = "Extent and focus of coaching provided by peers.";
    public const string EffortRelatedProcessCriteria = "Effort-Related Process Criteria";
    public const string StrategyRelatedProcessCriteria = "Strategy-Related Process Criteria";
    public const string KnowledgeAndSillRelatedProcessCriteria = "Knowledge-and-Skill-Related Process Criteria";
    public const string QualityOfTeamInteraction = "Quality of Team Interaction";
    public const string SatisfactionWithTeamRelationships = "Satisfaction with Team Relationships";
    public const string InternalWorkMotivation = "Internal Work Motivation";
    public const string SatisfactionWithGroupOpportunities = "Satisfaction with Group Opportunities";
    public const string GeneralSatisfaction = "General Satisfaction";
}

public static class HackmanSubComponent
{
    public const string Size = "Size";
    public const string Diversity = "Diversity";
    public const string Skills = "Skills";
    public const string WholeTask = "Whole Task";
    public const string Autonomy = "Autonomy";
    public const string KnowledgeOfResults = "Knowledge of Results";
    public const string GroupNorms = "Group Norms";
    public const string Overall = "Overall";
    public const string TaskFocusedCoaching = "Task-Focused Coaching";
    public const string OperantCoaching = "Operant Coaching";
    public const string InterpersonalCoaching = "Interpersonal Coaching";
    public const string UnhelpfulDirectives = "Unhelpful Directives";
    public const string TaskFocusedPeerCoaching = "Task-Focused Peer Coaching";
    public const string InterpersonalPeerCoaching = "Interpersonal Peer Coaching";
    public const string UnhelpfulPeerInterventions = "Unhelpful Peer Interventions";
}