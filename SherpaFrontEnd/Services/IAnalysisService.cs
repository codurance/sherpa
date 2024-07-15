using SherpaFrontEnd.Dtos.Analysis;

namespace SherpaFrontEnd.Services;

public interface IAnalysisService
{
    Task<GeneralResultsDto> GetGeneralResults(Guid teamId);
}