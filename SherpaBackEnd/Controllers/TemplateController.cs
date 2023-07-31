using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class TemplateController
{
    private readonly ITemplateService _templateService;

    public TemplateController(ITemplateService templateService)
    {
        _templateService = templateService;
    }

    public async Task<ActionResult<IEnumerable<Template>>> GetAllTemplates()
    {
        var allTemplates = await _templateService.GetAllTemplates();
        return new OkObjectResult(allTemplates);
    }
}