using SherpaBackEnd.Survey.Domain.Exceptions;

namespace SherpaBackEnd.Survey.Domain;

public class Survey
{
    public Guid Id { get; set; }
    public User.Domain.User Coach { get; set; }
    public SurveyStatus SurveyStatus { get; set; }
    public DateTime? Deadline { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public List<SurveyResponse> Responses { get; set; }
    public Team.Domain.Team Team { get; set; }
    public Template.Domain.Template Template { get; set; }

    public Survey(Guid id, User.Domain.User coach, SurveyStatus surveyStatus, DateTime? deadline, string title,
        string? description, List<SurveyResponse> responses, Team.Domain.Team team, Template.Domain.Template template)
    {
        Id = id;
        Coach = coach;
        SurveyStatus = surveyStatus;
        Deadline = deadline;
        Title = title;
        Description = description;
        Responses = responses;
        Team = team;
        Template = template;
    }

    public void AnswerSurvey(SurveyResponse response)
    {
        CanAnswerSurvey(response.TeamMemberId);
        CheckResponseIsComplete(response);

        Responses.Add(response);
    }

    private void CheckResponseIsComplete(SurveyResponse response)
    {
        // TODO: Make finer detailed check to make sure each question is answered
        if (Template.Questions.Count() != response.Answers.Count)
        {
            throw new SurveyNotCompleteException();
        }
    }

    private void CanAnswerSurvey(Guid teamMemberId)
    {
        if (!Team.IsMemberOfTeam(teamMemberId))
        {
            throw new SurveyNotAssignedToTeamMemberException(teamMemberId);
        }

        if (HasAlreadyResponded(teamMemberId))
        {
            throw new SurveyAlreadyAnsweredException(teamMemberId);
        }
    }

    private bool HasAlreadyResponded(Guid teamMemberId)
    {
        return Responses.Exists(response => response.TeamMemberId.Equals(teamMemberId));
    }
}