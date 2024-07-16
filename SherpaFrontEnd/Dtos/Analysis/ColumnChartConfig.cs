namespace SherpaFrontEnd.Dtos.Analysis;

public class ColumnChartConfig<T>
{
    public T MaxValue { get; }
    public T StepSize { get; }
    public int DecimalsInFloat { get; }

    public ColumnChartConfig(T maxValue, T stepSize, int decimalsInFloat)
    {
        MaxValue = maxValue;
        StepSize = stepSize;
        DecimalsInFloat = decimalsInFloat;
    }
}