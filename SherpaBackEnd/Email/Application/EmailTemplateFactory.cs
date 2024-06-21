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
        return createEmailTemplateDto switch
        {
            NewSurveyEmailTemplateDto newSurveyEmailTemplateDto => CreateNewSurveyEmailTemplate(
                newSurveyEmailTemplateDto),
            _ => throw new NotImplementedException()
        };
    }

    private NewSurveyEmailTemplate CreateNewSurveyEmailTemplate(NewSurveyEmailTemplateDto newSurveyEmailTemplateDto)
    {
        var survey = newSurveyEmailTemplateDto.SurveyNotifications.First().Survey;
        var title = survey.Title;
        var deadline = survey.Deadline;
        
        var recipients = newSurveyEmailTemplateDto.SurveyNotifications.Select(notification =>
        {
            var newSurveyTemplateModel = new NewSurveyTemplateModel()
            {
                Url = CreateAnswerSurveyUrl(notification),
                Deadline = deadline?.ToString("dd MMMM yyyy"),
                SurveyName = title,
                Name = notification.TeamMember.FullName
                
            };
            var html = new NewSurveyHtmlTemplate()
            {
                TemplateModel = newSurveyTemplateModel
            };
            var text = new NewSurveyTextTemplate()
            {
                TemplateModel = newSurveyTemplateModel
            };
            
            return new Recipient(notification.TeamMember.FullName, notification.TeamMember.Email,
                CreateAnswerSurveyUrl(notification))
            {
                HtmlBody = html.TransformText(),
                TextBody = text.TransformText(),
            };
        }).ToList();
        return new NewSurveyEmailTemplate(deadline, recipients);
    }

    private string CreateAnswerSurveyUrl(SurveyNotification.Domain.SurveyNotification notification)
    {
        var httpContextRequest = _httpContextAccessor.HttpContext?.Request;
        return $"{httpContextRequest?.Scheme}://{httpContextRequest?.Host}/answer-survey/{notification.Id}";
    }
}