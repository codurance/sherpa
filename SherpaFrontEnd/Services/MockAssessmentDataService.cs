using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class MockAssessmentDataService : IAssessmentsDataService
{
    public Task<List<OngoingAssessment>> GetAssessments()
    {
        Guid groupAId = Guid.NewGuid();
        Guid groupBId = Guid.NewGuid();

        return Task.FromResult(new List<OngoingAssessment>
        {
            new OngoingAssessment(Guid.NewGuid(), "Hackman Assessment",
                groupAId, "Group A",
                Guid.NewGuid(),
                new List<Survey>
                {
                    new Survey(DateOnly.Parse("2023-01-01")),
                    new Survey(DateOnly.FromDateTime(DateTime.Now))
                }),
            new OngoingAssessment(Guid.NewGuid(), "Skillset Assessment",
                groupAId, "Group A",
                Guid.NewGuid(),
                new List<Survey>
                {
                    new Survey(DateOnly.Parse("2023-01-01")),
                    new Survey(DateOnly.FromDateTime(DateTime.Now))
                }),
            new OngoingAssessment(Guid.NewGuid(), "Hackman Assessment",
                groupBId, "Group B",
                Guid.NewGuid(),
                new List<Survey>
                {
                    new Survey(DateOnly.Parse("2023-01-01")),
                    new Survey(DateOnly.FromDateTime(DateTime.Now))
                })
        });
    }
}