using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Template.Application;

namespace SherpaBackEnd.Template.Infrastructure.Http;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TemplateController
{
    private readonly ITemplateService _templateService;
    private readonly ILogger _logger;

    public TemplateController(ITemplateService templateService, ILogger<TemplateController> logger)
    {
        _templateService = templateService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Domain.Template>>> GetAllTemplatesAsync()
    {
        try
        {
            var allTemplates = await _templateService.GetAllTemplatesAsync();
            return new OkObjectResult(allTemplates);
        }
        catch (Exception e)
        {
            _logger.LogError(default, e, e.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}