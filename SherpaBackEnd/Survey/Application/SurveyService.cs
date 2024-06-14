using System.Runtime.InteropServices;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Domain.Exceptions;
using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Template.Infrastructure.Http.Dto;
using ISurveyRepository = SherpaBackEnd.Survey.Domain.Persistence.ISurveyRepository;

namespace SherpaBackEnd.Survey.Application;

public class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly ISurveyResponsesFileService _surveyResponsesFileService;
    public readonly Guid DefaultUserId = Guid.NewGuid();

    public SurveyService(ISurveyRepository surveyRepository, ITeamRepository teamRepository,
        ITemplateRepository templateRepository, ISurveyResponsesFileService surveyResponsesFileService)
    {
        _surveyRepository = surveyRepository;
        _teamRepository = teamRepository;
        _templateRepository = templateRepository;
        _surveyResponsesFileService = surveyResponsesFileService;
    }

    public async Task CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        var team = await _teamRepository.GetTeamByIdAsync(createSurveyDto.TeamId);
        var template = await _templateRepository.GetTemplateByName(createSurveyDto.TemplateName);

        if (team == null) throw new NotFoundException("Team not found");
        if (template == null) throw new NotFoundException("Template not found");

        var survey = new Domain.Survey(createSurveyDto.SurveyId, new User.Domain.User(DefaultUserId, "Lucia"),
            SurveyStatus.Draft,
            createSurveyDto.Deadline, createSurveyDto.Title, createSurveyDto.Description, new List<SurveyResponse>(),
            team,
            template);

        await _surveyRepository.CreateSurvey(survey);
    }

    public async Task<IEnumerable<Domain.Survey>> GetAllSurveysFromTeam(Guid teamId)
    {
        try
        {
            return await _surveyRepository.GetAllSurveysFromTeam(teamId);
        }
        catch (Exception error)
        {
            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);
        }
    }

    public async Task<SurveyWithoutQuestions> GetSurveyWithoutQuestionsById(Guid expectedSurveyId)
    {
        var surveyById = await GetSurveyById(expectedSurveyId);

        return new SurveyWithoutQuestions(surveyById.Id, surveyById.Coach, surveyById.SurveyStatus, surveyById.Deadline,
            surveyById.Title, surveyById.Description, surveyById.Responses, surveyById.Team,
            new TemplateWithoutQuestions(surveyById.Template.Name, surveyById.Template.MinutesToComplete));
    }

    public async Task<IEnumerable<IQuestion>> GetSurveyQuestionsBySurveyId(Guid expectedSurveyId)
    {
        var surveyById = await GetSurveyById(expectedSurveyId);
        return surveyById.Template.Questions;
    }

    public async Task AnswerSurvey(AnswerSurveyDto answerSurveyDto)
    {
        try
        {
            var survey = await GetSurveyById(answerSurveyDto.SurveyId);

            survey.AnswerSurvey(answerSurveyDto.Response);

            await _surveyRepository.Update(survey);
        }
        catch (Exception e)
        {
            switch (e)
            {
                case NotFoundException:
                case SurveyAlreadyAnsweredException:
                case SurveyNotAssignedToTeamMemberException:
                case SurveyNotCompleteException:
                    throw;
                default:
                    throw new ConnectionToRepositoryUnsuccessfulException("Unable to update answered survey");
            }
        }
    }

    public async Task<Stream> GetSurveyResponsesFileStream(Guid surveyId)
    {
        var survey = await GetSurveyById(surveyId);
        return _surveyResponsesFileService.CreateFileStream(survey);
    }

    private async Task<Domain.Survey?> GetSurveyById(Guid surveyId)
    {
        Domain.Survey survey;
        try
        {
            survey = await _surveyRepository.GetSurveyById(surveyId);
        }
        catch (Exception e)
        {
            throw new ConnectionToRepositoryUnsuccessfulException("Unable to retrieve survey from database", e);
        }
        
        if (survey == null)
        {
            throw new NotFoundException("Survey not found");
        }

        return survey;
    }
}