using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Helpers.Analysis;
using SherpaBackEnd.Survey.Domain.Persistence;

namespace SherpaBackEnd.Analysis.Application;

public class AnalysisService : IAnalysisService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ITemplateAnalysisRepository _templateAnalysisRepository;

    public AnalysisService(ISurveyRepository surveyRepository, ITemplateAnalysisRepository templateAnalysisRepository)
    {
        _surveyRepository = surveyRepository;
        _templateAnalysisRepository = templateAnalysisRepository;
    }

    public async Task<GeneralResultsDto> GetGeneralResults(Guid teamId)
    {
        // return GeneralResultsDtoBuilder.Build();
        
        // 1- Get the surveys
        // 2- Get the TemplateAnalysis
        // 3- We iterate the surveys and their responses
        
        // for each category we count the positives
        // mapper than creates the general-results
        
        // 1 collaborator that takes a list of surveys and returns a general-results object
        
        // -> for each response we categorize it
        // 4- Count all the participants and divide the positive results by them
        // 5- Build the ColumnChart

        await _surveyRepository.GetAllSurveysWithResponsesFromTeam(teamId);
        
        throw new NotImplementedException();
    }
}