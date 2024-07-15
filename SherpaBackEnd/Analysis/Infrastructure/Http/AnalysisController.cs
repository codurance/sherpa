using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Analysis.Application;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Analysis.Infrastructure.Http;

[ApiController]
[Route("")]
public class AnalysisController
{
    private readonly IAnalysisService _analysisService;

    public AnalysisController(IAnalysisService analysisService)
    {
        _analysisService = analysisService;
    }

    [HttpGet]
    public async Task<ActionResult<GeneralResultsDto>> GetGeneralResults()
    {
        var generalResults = await _analysisService.GetGeneralResults();
        return new OkObjectResult(generalResults);
    }
}