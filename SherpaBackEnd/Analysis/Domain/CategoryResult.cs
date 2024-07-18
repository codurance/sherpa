namespace SherpaBackEnd.Analysis.Domain;

public class CategoryResult
{
    public string Name { get; }
    
    public int NumberOfPositives { get; set; }
    public int TotalResponses { get; set; }

    public double PercentageOfPositives => Math.Round(NumberOfPositives / (double)TotalResponses, 2);
}