using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class TemplateController
{
    private readonly TemplateService _templateService;

    public TemplateController(TemplateService templateService)
    {
        _templateService = templateService;
    }

    public OkObjectResult GetAllTemplates()
    {
        throw new NotImplementedException();
    }
}