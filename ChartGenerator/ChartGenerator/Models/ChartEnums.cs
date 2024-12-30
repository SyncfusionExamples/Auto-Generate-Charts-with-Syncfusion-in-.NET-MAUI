namespace ChartGenerator;

public class ChartEnums
{
    public enum ChartTypeEnum
    {
        Cartesian, 
        Circular
    }

    public enum SeriesType
    {
        Line, 
        Column, 
        Spline, 
        Area, 
        Pie, 
        Doughnut, 
        RadialBar
    }

    public enum AxisType
    {
        Category, 
        Numerical, 
        DateTime, 
        Logarithmic
    }
}
