using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Analysis.Application;

public class AnalysisService : IAnalysisService
{
    private readonly IAnalysisRepository _analysisRepository;

    public AnalysisService(IAnalysisRepository analysisRepository)
    {
        _analysisRepository = analysisRepository;
    }

    public async Task<GeneralResultsDto> GetGeneralResults(Guid teamId)
    {
        var hackmanAnalysis =
            await _analysisRepository.GetAnalysisByTeamIdAndTemplateName(teamId, "Hackman Model");
        var generalResults = GeneralResultsDto.FromAnalysis(hackmanAnalysis);
        return generalResults;
    }
}