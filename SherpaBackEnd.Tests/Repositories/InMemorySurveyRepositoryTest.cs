using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Tests.Repositories;

public class InMemorySurveyRepositoryTest
{
    [Fact]
    public async Task ShouldBeAbleToCreateSurvey()
    {
        var inMemorySurveyRepository = new InMemorySurveyRepository();
        var survey = new Survey(Guid.NewGuid(), new User(Guid.NewGuid(), "Lucia"), Status.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "description", Array.Empty<Response>(),
            new Team(Guid.NewGuid(), "team name"), new Template("Template name", Array.Empty<IQuestion>(), 1));
        await inMemorySurveyRepository.CreateSurvey(survey);

        var retrievedSurvey = await inMemorySurveyRepository.GetSurveyById(survey.Id);
        
        Assert.Equal(survey, retrievedSurvey);
    }
}