using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Analysis.Infrastructure.Http;

[ApiController]
[Route("")]
public class AnalysisController
{
    [HttpGet]
    public async Task<ActionResult<GeneralResultsDto>> GetGeneralResults()
    {
        throw new NotImplementedException();
    }
}