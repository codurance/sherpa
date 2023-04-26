using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class MockAssessmentDataService : IAssessmentsDataService
{
    public Task<List<Assessment>> GetAssessments()
    {
        Guid groupAId = Guid.NewGuid();
        Guid groupBId = Guid.NewGuid();

        return Task.FromResult(new List<Assessment>
        {
            new ("Hackman Assessment",
                groupAId,
                Guid.NewGuid(),
                new List<Survey>
                {
                    new (DateOnly.Parse("2023-01-01")),
                    new (DateOnly.FromDateTime(DateTime.Now))
                }),
            new ("Skillset Assessment",
                groupAId, 
                Guid.NewGuid(),
                new List<Survey>
                {
                    new (DateOnly.Parse("2023-01-01")),
                    new (DateOnly.FromDateTime(DateTime.Now))
                }),
            new ("Hackman Assessment",
                groupBId, 
                Guid.NewGuid(),
                new List<Survey>
                {
                    new (DateOnly.Parse("2023-01-01")),
                    new (DateOnly.FromDateTime(DateTime.Now))
                })
        });
    }
}