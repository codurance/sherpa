namespace SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

public class SurveyMetric
{
    public string Name { get; }
    public double Average { get; }

    public SurveyMetric(string name, double average)
    {
        Name = name;
        Average = average;
    }
}