namespace SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

public class ColumnChartConfig<T>
{
    public T MaxValue { get; }
    public double StepSize { get; }
    public int DecimalsInFloat { get; }

    public ColumnChartConfig(T maxValue, double stepSize, int decimalsInFloat)
    {
        MaxValue = maxValue;
        StepSize = stepSize;
        DecimalsInFloat = decimalsInFloat;
    }
}