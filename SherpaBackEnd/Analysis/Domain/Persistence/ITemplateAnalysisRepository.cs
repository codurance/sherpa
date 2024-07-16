namespace SherpaBackEnd.Analysis.Domain.Persistence;

public interface ITemplateAnalysisRepository
{
    Task<TemplateAnalysis> GetTemplateAnalysisByName(string name);
}