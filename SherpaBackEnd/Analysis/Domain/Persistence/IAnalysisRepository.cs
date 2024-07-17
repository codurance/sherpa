namespace SherpaBackEnd.Analysis.Domain.Persistence;

public interface IAnalysisRepository
{
    Task<TemplateAnalysis> GetTemplateAnalysisByName(string name);

    Task<HackmanAnalysis> GetAnalysisByTeamIdAndTemplateName(Guid teamId, string name);
}