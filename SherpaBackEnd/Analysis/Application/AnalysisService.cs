using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Helpers.Analysis;
using SherpaBackEnd.Survey.Domain.Persistence;

namespace SherpaBackEnd.Analysis.Application;

public class AnalysisService : IAnalysisService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly IAnalysisRepository _analysisRepository;

    public AnalysisService(ISurveyRepository surveyRepository, IAnalysisRepository analysisRepository)
    {
        _surveyRepository = surveyRepository;
        _analysisRepository = analysisRepository;
    }

    public async Task<GeneralResultsDto> GetGeneralResults(Guid teamId)
    {
        // return GeneralResultsDtoBuilder.Build();
        
        // var surveyResult =
        //     _analysisRepository.GetSurveyResultsByTeamIdAndTemplateName(teamId, "Hackman template");
        // var generalResults = GeneralResultsDto.fromSurveyResult(surveyResult);
        // return generalResults;

        await _surveyRepository.GetAllSurveysWithResponsesFromTeam(teamId);
        
        throw new NotImplementedException();
    }
}