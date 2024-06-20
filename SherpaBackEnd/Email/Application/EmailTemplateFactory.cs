using SherpaBackEnd.Email.Templates;
using SherpaBackEnd.Email.Templates.NewSurvey;

namespace SherpaBackEnd.Email.Application;

public class EmailTemplateFactory : IEmailTemplateFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmailTemplateFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public EmailTemplate CreateEmailTemplate(CreateEmailTemplateDto createEmailTemplateDto)
    {
        switch (createEmailTemplateDto)
        {
            case NewSurveyEmailTemplateDto newSurveyEmailTemplateDto:
            {
                var survey = newSurveyEmailTemplateDto.SurveyNotifications.First().Survey;
                var title = survey.Title;
                var deadline = survey.Deadline;
                var recipients = newSurveyEmailTemplateDto.SurveyNotifications.Select(notification =>
                    new Recipient(notification.TeamMember.FullName, notification.TeamMember.Email,
                        CreateAnswerSurveyUrl(notification))).ToList();
                return new EmailTemplate("NewSurvey", title, deadline, recipients);
            }
            default:
                throw new NotImplementedException();
        }
    }

    private string CreateAnswerSurveyUrl(SurveyNotification.Domain.SurveyNotification notification)
    {
        var httpContextRequest = _httpContextAccessor.HttpContext?.Request;
        return $"{httpContextRequest?.Scheme}://{httpContextRequest?.Host}/answer-survey/{notification.Id}";
    }
}