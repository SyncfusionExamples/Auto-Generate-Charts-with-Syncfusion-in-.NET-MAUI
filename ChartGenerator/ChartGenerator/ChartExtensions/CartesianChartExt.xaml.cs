using Syncfusion.Maui.Toolkit.Charts;
using System.Collections.ObjectModel;

namespace ChartGenerator;

public partial class CartesianChartExt : SfCartesianChart
{
    public CartesianChartExt()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(ObservableCollection<SeriesConfig>), typeof(CartesianChartExt), null, BindingMode.Default, null, OnPropertyChanged);
    public ObservableCollection<SeriesConfig> Source
    {
        get => (ObservableCollection<SeriesConfig>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (Equals(oldValue, newValue))
        {
            return;
        }

        if (bindable is CartesianChartExt chart)
        {
            chart.GenerateSeries(newValue as ObservableCollection<SeriesConfig>);
        }
    }

    private void GenerateSeries(ObservableCollection<SeriesConfig> configs)
    {
        if (configs != null)
        {
            this.Series.Clear();
            foreach (var config in configs)
            {
                //CreateSeries(config);
                CreateSeriesFromTemplate(config);
            }
        }
    }

    private void CreateSeriesFromTemplate(SeriesConfig seriesConfig)
    {
        var templateSelector = (SeriesTemplateSelector)Resources["seriesTemplateSelector"];
        var template = templateSelector.SelectTemplate(seriesConfig, null);

        if (template != null)
        {
            ChartSeries series = (ChartSeries)template.CreateContent();

            if (series != null)
            {
                series.BindingContext = seriesConfig;
                this.Series.Add(series);
            }
        }
    }

    private void CreateSeries(SeriesConfig config)
    {
        ChartSeries series = null;

        switch (config.Type)
        {
            case ChartEnums.SeriesType.Line:
                series = new LineSeries
                {
                    ItemsSource = config.DataSource,
                    XBindingPath = config.XPath,
                    YBindingPath = "yvalue",
                    EnableTooltip = config.Tooltip
                };
                break;

            case ChartEnums.SeriesType.Area:
                series = new AreaSeries
                {
                    ItemsSource = config.DataSource,
                    XBindingPath = config.XPath,
                    YBindingPath = "yvalue",
                    EnableTooltip = config.Tooltip
                };
                break;

            case ChartEnums.SeriesType.Spline:
                series = new SplineSeries
                {
                    ItemsSource = config.DataSource,
                    XBindingPath = config.XPath,
                    YBindingPath = "yvalue",
                    EnableTooltip = config.Tooltip
                };
                break;

            case ChartEnums.SeriesType.Column:
                series = new ColumnSeries
                {
                    ItemsSource = config.DataSource,
                    XBindingPath = config.XPath,
                    YBindingPath = "yvalue",
                    EnableTooltip = config.Tooltip
                };
                break;
        }

        if (series != null)
        {
            this.Series.Add(series);
        }
    }
}
