namespace ChartGenerator;

public partial class ChartView : ContentPage
{
    public ChartView(ChartViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}

public class ChartTemplateSelector : DataTemplateSelector
{
    public DataTemplate CartesianChartTemplate { get; set; }
    public DataTemplate CircularChartTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is ChartConfig chartConfig)
        {
            return chartConfig.ChartType switch
            {
                ChartEnums.ChartTypeEnum.Cartesian => CartesianChartTemplate,
                ChartEnums.ChartTypeEnum.Circular => CircularChartTemplate,
                _ => null
            };
        }
        return null;
    }
}