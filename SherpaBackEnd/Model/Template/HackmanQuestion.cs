namespace SherpaBackEnd.Model.Template;

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

public static class HackmanSubcomponent
{
    public static readonly string REAL_TEAM = "Real Team";
    public static readonly string COMPELLING_DIRECTION = "Compelling Direction";
    public static readonly string ENABLING_STRUCTURE = "Enabling Structure";
    public static readonly string SUPPORTIVE_ORGANIZATIONAL_CONTEXT = "Supportive Organizational Context";
    public static readonly string EXPERT_COACHING = "Expert Coaching";
    public static readonly string SENSE_OF_URGENCY = "Sense Of Urgency";
}

public static class HackmanSubcategory
{
    public static readonly string DELIMITED = "Delimited";
    public static readonly string INTERDEPENDENT = "Interdependent";
    public static readonly string STABLE = "Stable";
    public static readonly string CLEAR = "Clear";
    public static readonly string CHALLENGING = "Challenging";
    public static readonly string CONSECUENTIAL = "Consecuential";
    public static readonly string ENDS_VS_MEANS = "Ends Vs Means";
    public static readonly string TEAM_COMPOSITION = "Team Composition";
    public static readonly string TEAM_TASK_DESIGN = "Team Task Design";
    public static readonly string REWARDS_RECOGNITION = "Rewards/Recognition";
    public static readonly string INFORMATION = "Information";
    public static readonly string EDUCATION_CONSULTATION = "Education/Consultation";
    public static readonly string MATERIAL_RESOURCES = "Material/Resources";
    public static readonly string FOCUS_OF_LEADERS_ATTENTION = "Focus of Leader’s Attention";
    public static readonly string COACHING_AVAILABILITY = "Coaching Availability";
    public static readonly string HELFULNESS_OF_TEAM_LEADER_COACHING = "Helpfulness of team leader coaching";
    public static readonly string EXTENT_AND_FOCUS_OF_COACHING_PROVIDED_BY_PEERS = "Extent and focus of coaching provided by peers.";
    public static readonly string EFFORT_RELATED_PROCESS_CRITERIA = "Effort-Related Process Criteria";
    public static readonly string STRATEGY_RELATED_PROCESS_CRITERIA = "Strategy-Related Process Criteria";
    public static readonly string KNOWLEDGE_AND_SILL_RELATED_PROCESS_CRITERIA = "Knowledge-and-Skill-Related Process Criteria";
    public static readonly string QUALITY_OF_TEAM_INTERACTION = "Quality of Team Interaction";
    public static readonly string SATISFACTION_WITH_TEAM_RELATIONSHIPS = "Satisfaction with Team Relationships";
    public static readonly string INTERNAL_WORK_MOTIVATION = "Internal Work Motivation";
    public static readonly string SATISFACTION_WITH_GROUP_OPPORTUNITIES = "Satisfaction with Group Opportunities";
    public static readonly string GENERAL_SATISFACTION = "General Satisfaction";
}

public static class HackmanComponent
{
    public static readonly string SIZE = "Size";
    public static readonly string DIVERSITY = "Diversity";
    public static readonly string SKILLS = "Skills";
    public static readonly string WHOLE_TASK = "Whole Task";
    public static readonly string AUTONOMY = "Autonomy";
    public static readonly string KNOWLEDGE_OF_RESULTS = "Knowledge of Results";
    public static readonly string GROUP_NORMS = "Group Norms";
    public static readonly string OVERALL = "Overall";
    public static readonly string TASK_FOCUSED_COACHING = "Task-Focused Coaching";
    public static readonly string OPERANT_COACHING = "Operant Coaching";
    public static readonly string INTERPERSONAL_COACHING = "Interpersonal Coaching";
    public static readonly string UNHELPFUL_DIRECTIVES = "Unhelpful Directives";
    public static readonly string TASK_FOCUSED_PEER_COACHING = "Task-Focused Peer Coaching";
    public static readonly string INTERPERSONAL_PEER_COACHING = "Interpersonal Peer Coaching";
    public static readonly string UNHELPFUL_PEER_INTERVENTIONS = "Unhelpful Peer Interventions";
}