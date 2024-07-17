using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Analysis.Domain;

public class SurveyResult<T>
{
    public string Title { get; }
    public List<string> Categories { get; set; } = new();
    public int NumberOfParticipants { get; set; }
    public Dictionary<string, CategoryResult> CategoryResults = new();

    public SurveyResult(string title)
    {
        Title = title;
    }

    public void AddResponse(Response<T> response)
    {
        if (!CategoryResults.ContainsKey(response.Category))
        {
            CategoryResults.Add(response.Category, new CategoryResult());

        }
        if (response.IsPositive())
        {
            CategoryResults[response.Category].NumberOfPositives++;
        }
    }
    
}