using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Analysis.Application;
using SherpaBackEnd.Analysis.Domain.Exceptions;
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

    [Authorize]
    [HttpGet("team/{teamId:guid}/analysis/general-results")]
    public async Task<ActionResult<GeneralResultsDto>> GetGeneralResults(Guid teamId)
    {
        try
        {
            var generalResults = await _analysisService.GetGeneralResults(teamId);
            return new OkObjectResult(generalResults);
        }
        catch (TeamNotFoundException error)
        {
            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status404NotFound
            };
        }
    }
}