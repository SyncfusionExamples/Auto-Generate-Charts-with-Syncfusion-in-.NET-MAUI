using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows.Input;

namespace ChartGenerator
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? entryText;
        private ContentView? chart;
        private bool showAssistView;
        private ChartConfig chartData;

        public string? EntryText
        {
            get => entryText;
            set
            {
                entryText = value;
                OnPropertyChanged(nameof(EntryText));
            }
        }

        public ContentView? Chart
        {
            get => chart;
            set
            {
                chart = value;
                OnPropertyChanged(nameof(Chart));
            }
        }

        public bool ShowAssistView
        {
            get => showAssistView;
            set
            {
                showAssistView = value;
                OnPropertyChanged(nameof(ShowAssistView));
            }
        }

        public ChartConfig ChartData
        {
            get => chartData;
            set
            {
                chartData = value;
                OnPropertyChanged(nameof(ChartData));
            }
        }

        public ICommand ButtonClicked { get; }
        public ICommand CreateButtonClicked { get; }

        public ChartViewModel()
        {
            ButtonClicked = new Command<string>(OnButtonClicked);
            CreateButtonClicked = new Command(OnCreateButtonClicked);
        }

        private void OnButtonClicked(string buttonText)
        {
            EntryText = ReturnOfflineEditorText(buttonText);
        }

        private async void OnCreateButtonClicked()
        {
            if (!string.IsNullOrEmpty(EntryText))
            {
                DecryptJSON(EntryText);

                Application.Current.MainPage.Navigation.PushAsync(new ChartView(this));
            }
            else
            {
                EntryText = "Invalid JSON, Try Again";
            }
        }

        internal string ReturnOfflineEditorText(string entryText)
        {
            string response = string.Empty;

            if (entryText.Contains("column"))
            {
                response = "{\n  \"chartType\": \"cartesian\",\n  \"title\": \"Revenue by Region\",\n  \"xAxis\": {\n    \"type\": \"category\",\n    \"title\": \"Region\"\n  },\n  \"yAxis\": {\n    \"title\": \"Revenue\",\n    \"type\": \"numerical\",\n    \"min\": 0\n  },\n  \"series\": [\n    {\n      \"type\": \"column\",\n      \"xpath\": \"region\",\n      \"dataSource\": [\n        { \"xvalue\": \"North America\", \"yvalue\": 120000 },\n        { \"xvalue\": \"Europe\", \"yvalue\": 90000 },\n        { \"xvalue\": \"Asia\", \"yvalue\": 70000 },\n        { \"xvalue\": \"South America\", \"yvalue\": 45000 },\n        { \"xvalue\": \"Australia\", \"yvalue\": 30000 }\n      ],\n      \"tooltip\": true\n    }\n  ]\n}";
            }
            else if (entryText.Contains("area"))
            {
                response = "{\n  \"chartType\": \"cartesian\",\n  \"title\": \"Farm Productivity Over Different Seasons\",\n  \"xAxis\": {\n    \"type\": \"category\",\n    \"title\": \"Seasons\"\n  },\n  \"yAxis\": {\n    \"title\": \"Productivity (tons)\",\n    \"type\": \"numerical\",\n    \"min\": 0\n, \n    \"max\": 200\n   },\n  \"series\": [\n    {\n      \"type\": \"area\",\n      \"xpath\": \"season\",\n      \"dataSource\": [\n        { \"xvalue\": \"Spring\", \"yvalue\": 120 },\n        { \"xvalue\": \"Summer\", \"yvalue\": 150 },\n        { \"xvalue\": \"Autumn\", \"yvalue\": 130 },\n        { \"xvalue\": \"Winter\", \"yvalue\": 80 }\n      ],\n      \"tooltip\": true\n    }\n  ]\n}";
            }
            else if (entryText.Contains("pie"))
            {
                response = "{\n  \"chartType\": \"circular\",\n  \"title\": \"Monthly Sales Data\",\n  \"series\": [\n    {\n      \"type\": \"pie\",\n      \"xpath\": \"category\",\n      \"dataSource\": [\n        { \"xvalue\": \"January\", \"yvalue\": 5000 },\n        { \"xvalue\": \"February\", \"yvalue\": 6000 },\n        { \"xvalue\": \"March\", \"yvalue\": 7000 },\n        { \"xvalue\": \"April\", \"yvalue\": 8000 },\n        { \"xvalue\": \"May\", \"yvalue\": 9000 },\n        { \"xvalue\": \"June\", \"yvalue\": 10000 },\n        { \"xvalue\": \"July\", \"yvalue\": 11000 },\n        { \"xvalue\": \"August\", \"yvalue\": 12000 },\n        { \"xvalue\": \"September\", \"yvalue\": 13000 },\n        { \"xvalue\": \"October\", \"yvalue\": 14000 },\n        { \"xvalue\": \"November\", \"yvalue\": 15000 },\n        { \"xvalue\": \"December\", \"yvalue\": 16000 }\n      ],\n      \"tooltip\": true\n    }\n  ]\n}";
            }
            else if (entryText.Contains("doughnut"))
            {
                response = "{\n  \"chartType\": \"circular\",\n  \"title\": \"Task Completion Doughnut Chart\",\n  \"series\": [\n    {\n      \"type\": \"doughnut\",\n      \"xpath\": \"xvalue\",\n      \"dataSource\": [\n        { \"xvalue\": \"Completed\", \"yvalue\": 70 },\n        { \"xvalue\": \"In Progress\", \"yvalue\": 20 },\n        { \"xvalue\": \"Pending\", \"yvalue\": 10 }\n      ],\n      \"tooltip\": true\n    }\n  ],\n  \"xAxis\": {\n    \"type\": \"category\",\n    \"title\": \"Task Status\"\n  },\n  \"yAxis\": {\n    \"title\": \"Percentage\",\n    \"type\": \"numerical\",\n    \"min\": 0,\n    \"max\": 100\n  }\n}";
            }
            else if (entryText.Contains("spline"))
            {
                response = "{\n  \"chartType\": \"cartesian\",\n  \"title\": \"Daily Average Temperature for the Past Week\",\n  \"series\": [\n    {\n      \"type\": \"spline\",\n      \"xpath\": \"date\",\n      \"dataSource\": [\n        { \"xvalue\": \"2023-10-01\", \"yvalue\": 15 },\n        { \"xvalue\": \"2023-10-02\", \"yvalue\": 17 },\n        { \"xvalue\": \"2023-10-03\", \"yvalue\": 16 },\n        { \"xvalue\": \"2023-10-04\", \"yvalue\": 18 },\n        { \"xvalue\": \"2023-10-05\", \"yvalue\": 20 },\n        { \"xvalue\": \"2023-10-06\", \"yvalue\": 19 },\n        { \"xvalue\": \"2023-10-07\", \"yvalue\": 21 }\n      ],\n      \"tooltip\": true\n    }\n  ],\n  \"xAxis\": {\n    \"type\": \"category\",\n    \"title\": \"Date\"\n  },\n  \"yAxis\": {\n    \"title\": \"Temperature (°C)\",\n    \"type\": \"numerical\",\n    \"min\": 0,\n    \"max\": 30\n  }\n}";
            }
            else if (entryText.Contains("line"))
            {
                response = "\n{\n  \"chartType\": \"cartesian\",\n  \"title\": \"Product Sales Over Six Months\",\n  \"series\": [\n    {\n      \"type\": \"line\",\n      \"xpath\": \"month\",\n      \"dataSource\": [\n        { \"xvalue\": \"January\", \"yvalue\": 150 },\n        { \"xvalue\": \"February\", \"yvalue\": 200 },\n        { \"xvalue\": \"March\", \"yvalue\": 175 },\n        { \"xvalue\": \"April\", \"yvalue\": 220 },\n        { \"xvalue\": \"May\", \"yvalue\": 240 },\n        { \"xvalue\": \"June\", \"yvalue\": 210 }\n      ],\n      \"tooltip\": true\n    }\n  ],\n  \"xAxis\": {\n    \"type\": \"category\",\n    \"title\": \"Month\"\n  },\n  \"yAxis\": {\n    \"title\": \"Sales (Units)\",\n    \"type\": \"numerical\",\n    \"min\": 0,\n    \"max\": 300\n  }\n}\n";
            }
            else
            {
                response = "{\n  \"chartType\": \"cartesian\",\n  \"title\": \"Revenue by Region\",\n  \"xAxis\": {\n    \"type\": \"category\",\n    \"title\": \"Region\"\n  },\n  \"yAxis\": {\n    \"title\": \"Revenue\",\n    \"type\": \"numerical\",\n    \"min\": 0\n  },\n  \"series\": [\n    {\n      \"type\": \"column\",\n      \"xpath\": \"region\",\n      \"dataSource\": [\n        { \"xvalue\": \"North America\", \"yvalue\": 120000 },\n        { \"xvalue\": \"Europe\", \"yvalue\": 90000 },\n        { \"xvalue\": \"Asia\", \"yvalue\": 70000 },\n        { \"xvalue\": \"South America\", \"yvalue\": 45000 },\n        { \"xvalue\": \"Australia\", \"yvalue\": 30000 }\n      ],\n      \"tooltip\": true\n    }\n  ]\n}";
            }

            return response;
        }

        internal void DecryptJSON(string jsonData)
        {
            try
            {
                var chartData = JsonConvert.DeserializeObject<ChartConfig>(jsonData);

                ChartData = chartData!;
            }
            catch (JsonException ex)
            {
                EntryText = "Invalid JSON, Try Again";
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal async void PrintJson(string response)
        {
            await Task.Delay(5000);

            EntryText = string.Empty;
            foreach (char c in response)
            {
                EntryText += c;
                await Task.Delay(50); // half a second delay for each character
            }
        }
    }
}
