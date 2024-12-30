using Syncfusion.Maui.Toolkit.Charts;
using System.Collections.ObjectModel;

namespace ChartGenerator;

public partial class CircularChartExt : SfCircularChart
{
    public CircularChartExt()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(ObservableCollection<SeriesConfig>), typeof(CircularChartExt), null, BindingMode.Default, null, OnPropertyChanged);
    public ObservableCollection<SeriesConfig> Source
    {
        get => (ObservableCollection<SeriesConfig>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CircularChartExt chart)
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

    private void CreateSeriesFromTemplate(SeriesConfig config)
    {
        var templateSelector = (SeriesTemplateSelector)Resources["seriesTemplateSelector"];
        var template = templateSelector.SelectTemplate(config, null);

        if (template != null)
        {
            ChartSeries series = (ChartSeries)template.CreateContent();

            if (series != null)
            {
                series.BindingContext = config;
                this.Series.Add(series);
            }
        }
    }

    private void CreateSeries(SeriesConfig config)
    {
        ChartSeries series = null;

        switch (config.Type)
        {
            case ChartEnums.SeriesType.Pie:
                series = new PieSeries
                {
                    ItemsSource = config.DataSource,
                    XBindingPath = config.XPath,
                    YBindingPath = "yvalue",
                    EnableTooltip = config.Tooltip
                };
                break;

            case ChartEnums.SeriesType.Doughnut:
                series = new DoughnutSeries
                {
                    ItemsSource = config.DataSource,
                    XBindingPath = config.XPath,
                    YBindingPath = "yvalue",
                    EnableTooltip = config.Tooltip
                };
                break;

            case ChartEnums.SeriesType.RadialBar:
                series = new RadialBarSeries
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