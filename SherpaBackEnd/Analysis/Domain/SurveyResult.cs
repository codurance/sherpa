using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Analysis.Domain;

public class SurveyResult<T>
{
    public string Title { get; }
    public List<string> Categories { get; set; }
    public int NumberOfParticipants { get; set; }
    public Dictionary<string, CategoryResult> CategoryResults = new();

    public SurveyResult(string title)
    {
        Title = title;
    }

    public void AddResponse(Response<T> response)
    {
        // Perform some logic to check if it's possitive
        response.IsPositive();
        if (CategoryResults.ContainsKey(response.Category))
        {
            // CategoryResults[response.Category].NumberOfPositives++;
        }
        else
        {
            CategoryResults.Add(response.Category, new CategoryResult());
        }
    }
    
}