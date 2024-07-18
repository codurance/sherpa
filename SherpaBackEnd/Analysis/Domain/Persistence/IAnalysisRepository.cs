namespace SherpaBackEnd.Analysis.Domain.Persistence;

public interface IAnalysisRepository
{
    Task<HackmanAnalysis> GetAnalysisByTeamIdAndTemplateName(Guid teamId, string templateName);
}