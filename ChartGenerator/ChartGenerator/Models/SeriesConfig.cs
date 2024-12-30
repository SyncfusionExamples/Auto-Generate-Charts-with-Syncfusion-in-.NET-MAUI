using System.Collections.ObjectModel;

namespace ChartGenerator;

public class SeriesConfig
{
    public ChartEnums.SeriesType Type 
    {
        get; 
        set;
    }

    public string XPath
    {
        get;
        set;
    }

    public ObservableCollection<DataModel> DataSource 
    {
        get; 
        set;
    }

    public bool Tooltip
    {
        get;
        set;
    }
}

public class DataModel
{
    public string xvalue
    {
        get; 
        set;
    }

    public double yvalue
    {
        get; 
        set;
    }

    public DateTime? date
    {
        get; 
        set;
    }

    public double? xval
    {
        get; 
        set;
    }
}