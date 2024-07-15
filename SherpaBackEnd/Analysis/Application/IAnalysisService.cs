using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Analysis.Application;

public interface IAnalysisService
{
    Task<GeneralResultsDto> GetGeneralResults();
}