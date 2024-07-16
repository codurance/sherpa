namespace SherpaBackEnd.Analysis.Domain.Persistence;

public interface IAnalysisRepository
{
    Task<TemplateAnalysis> GetTemplateAnalysisByName(string name);

    Task<SurveyResult> GetSurveyResultsByTeamIdAndTemplateName(Guid teamId, string name);
}