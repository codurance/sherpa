using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Repositories;

namespace SherpaBackEnd.Tests.Repositories;

public class InMemorySurveyRepositoryTest
{
    [Fact]
    public async Task ShouldReturnAllSurveysFromTeam()
    {
        var teamOneId = Guid.NewGuid();
        const string teamOneName = "Team One";
        var teamOne = new Team(teamOneId, teamOneName);
        
        var teamTwoId = Guid.NewGuid();
        const string teamTwoName = "Team Two";
        var teamTwo = new Team(teamTwoId, teamTwoName);

        var userOne = new User(Guid.NewGuid(), "User One");
        var hackManTemplate = new Template("HackMan Template", new IQuestion[] { }, 3600);
        var initialSurveys = new List<Survey>(){
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "Survey One", "This is the survey one.", new Response[] { }, teamOne, hackManTemplate),
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "Survey Two", "This is the survey two.", new Response[] { }, teamTwo, hackManTemplate),
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "Survey Three", "This is the survey three.", new Response[] { }, teamOne, hackManTemplate),
        };
        
        var inMemorySurveyRepository = new InMemorySurveyRepository(initialSurveys);

        var foundSurveys = await inMemorySurveyRepository.GetAllSurveysFromTeam(teamOneId);

        var expectedAmountOfSurveysForTeamOne = 2;
        Assert.True(foundSurveys.Count() == expectedAmountOfSurveysForTeamOne);
        Assert.Equal(teamOne, foundSurveys.First().Team);
    }
    
    [Fact]
    public async Task ShouldReturnEmptyListOfSurveysIfNotFoundAnyMatchingByTeamId()
    {
        var teamOneId = Guid.NewGuid();
        const string teamOneName = "Team One";
        var teamOne = new Team(teamOneId, teamOneName);
        
        var teamTwoId = Guid.NewGuid();
        const string teamTwoName = "Team Two";
        var teamTwo = new Team(teamTwoId, teamTwoName);

        var userOne = new User(Guid.NewGuid(), "User One");
        var hackManTemplate = new Template("HackMan Template", new IQuestion[] { }, 3600);
        var initialSurveys = new List<Survey>(){
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "Survey One", "This is the survey one.", new Response[] { }, teamTwo, hackManTemplate),
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "Survey Two", "This is the survey two.", new Response[] { }, teamTwo, hackManTemplate),
        };
        
        var inMemorySurveyRepository = new InMemorySurveyRepository(initialSurveys);

        var foundSurveys = await inMemorySurveyRepository.GetAllSurveysFromTeam(teamOneId);
        
        Assert.True(!foundSurveys.Any());
    }
}