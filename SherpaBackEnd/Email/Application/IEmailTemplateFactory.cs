using SherpaBackEnd.Email.Templates;

namespace SherpaBackEnd.Email.Application;

public interface IEmailTemplateFactory
{
    EmailTemplate CreateEmailTemplate(CreateEmailTemplateDto createEmailTemplateDto);
}