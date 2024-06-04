using Microsoft.AspNetCore.SignalR;
using SherpaBackEnd.Survey.Domain;
using SherpaFrontEnd.Dtos.Survey;

namespace SherpaBackEnd.Tests.Builders;

public class SurveyBuilder
{
    private Guid _id = Guid.NewGuid();
    private User.Domain.User _coach = new User.Domain.User(Guid.NewGuid(), "Lucia");
    private SurveyStatus _status = SurveyStatus.Draft;
    private DateTime _deadline;
    private string _title = "Survey";
    private string? _description = "";
    private List<SurveyResponse> _responses = new List<SurveyResponse>();
    private Team.Domain.Team _team;
    private Template.Domain.Template _template;

    public static SurveyBuilder ASurvey()
    {
        return new SurveyBuilder();
    }

    public SurveyBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public SurveyBuilder WithStatus(SurveyStatus status)
    {
        _status = status;
        return this;
    }

    public SurveyBuilder WithCoach(User.Domain.User coach)
    {
        _coach = coach;
        return this;
    }

    public SurveyBuilder WithDeadline(DateTime deadline)
    {
        _deadline = deadline;
        return this;
    }

    public SurveyBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public SurveyBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public SurveyBuilder WithResponses(List<SurveyResponse> responses)
    {
        _responses = responses;
        return this;
    }

    public SurveyBuilder WithTeam(Team.Domain.Team team)
    {
        _team = team;
        return this;
    }

    public SurveyBuilder WithTemplate(Template.Domain.Template template)
    {
        _template = template;
        return this;
    }

    public Survey.Domain.Survey Build()
    {
        return new Survey.Domain.Survey(_id, _coach, _status, _deadline, _title, _description, _responses, _team,
            _template);
    }
}