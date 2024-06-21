namespace SherpaBackEnd.Email.Templates;

public abstract class CreateEmailTemplateDto
{
    public string TemplateName { get; set; }

    protected CreateEmailTemplateDto(string templateName)
    {
        TemplateName = templateName;
    }
}