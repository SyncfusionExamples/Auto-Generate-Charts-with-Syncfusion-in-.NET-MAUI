namespace ChartGenerator;

public partial class SeriesTemplates : ResourceDictionary
{
	public SeriesTemplates()
	{
		InitializeComponent();
	}
}

public class SeriesTemplateSelector : DataTemplateSelector
{
    public DataTemplate LineSeriesTemplate { get; set; }
    public DataTemplate SplineSeriesTemplate { get; set; }
    public DataTemplate ColumnSeriesTemplate { get; set; }
    public DataTemplate AreaSeriesTemplate { get; set; }
    public DataTemplate PieSeriesTemplate { get; set; }
    public DataTemplate DoughnutSeriesTemplate { get; set; }
    public DataTemplate RadialBarSeriesTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is SeriesConfig config)
        {
            switch (config.Type)
            {
                case ChartEnums.SeriesType.Line:
                    return LineSeriesTemplate;
                case ChartEnums.SeriesType.Spline:
                    return SplineSeriesTemplate;
                case ChartEnums.SeriesType.Column:
                    return ColumnSeriesTemplate;
                case ChartEnums.SeriesType.Area:
                    return AreaSeriesTemplate;
                case ChartEnums.SeriesType.Pie:
                    return PieSeriesTemplate;
                case ChartEnums.SeriesType.Doughnut:
                    return DoughnutSeriesTemplate;
                case ChartEnums.SeriesType.RadialBar:
                    return RadialBarSeriesTemplate;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return null;
    }
}
