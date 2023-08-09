﻿using Moq;
using Newtonsoft.Json;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;
using SherpaBackEnd.Services.Email;
using Xunit.Abstractions;
using ISurveyRepository = SherpaBackEnd.Model.ISurveyRepository;

namespace SherpaBackEnd.Tests.Services;

public class SurveyServiceTest
{
    private Mock<ISurveyRepository> _surveyRepo;
    private Mock<ITeamRepository> _teamRepo;
    private Mock<ITemplateRepository> _templateRepo;
    private readonly ITestOutputHelper output;

    public SurveyServiceTest(ITestOutputHelper output)
    {
        this.output = output;
        _surveyRepo = new Mock<ISurveyRepository>();
        _teamRepo = new Mock<ITeamRepository>();
        _templateRepo = new Mock<ITemplateRepository>();
    }

    [Fact]
    public async Task ShouldRetrieveTemplateAndTeamFromTheirReposAndPersistSurveyOnSurveyRepoWhenCallingCreateSurvey()
    {
        var team = new Team(Guid.NewGuid(), "Demo team");
        var template = new Template("demo", Array.Empty<IQuestion>(), 30);

        var service = new SurveyService(_surveyRepo.Object, _teamRepo.Object, _templateRepo.Object);

        _teamRepo.Setup(repository => repository.GetTeamByIdAsync(team.Id)).ReturnsAsync(team);
        _templateRepo.Setup(repository => repository.GetTemplateByName(template.Name)).ReturnsAsync(template);

        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), team.Id, template.Name, "Title", "Description", DateTime.Parse("2023-08-09T07:38:04+0000") );
        await service.CreateSurvey(createSurveyDto);
        
        var expectedSurvey = new Survey(createSurveyDto.SurveyId, new User(service.DefaultUserId, "Lucia"), Status.Draft, createSurveyDto.Deadline, createSurveyDto.Title, createSurveyDto.Description, Array.Empty<Response>(), team, template);
        var surveyRepoInvocation = _surveyRepo.Invocations[0];
        var actualSurvey = surveyRepoInvocation.Arguments[0];
        Assert.Equal(JsonConvert.SerializeObject(expectedSurvey), JsonConvert.SerializeObject(actualSurvey));
    }
}