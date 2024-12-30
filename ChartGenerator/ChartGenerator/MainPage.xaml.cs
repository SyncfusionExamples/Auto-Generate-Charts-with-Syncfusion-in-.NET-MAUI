namespace ChartGenerator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    var response = "{\n  \"chartType\": \"circular\",\n  \"title\": \"Monthly Sales Data\",\n  \"series\": [\n    {\n      \"type\": \"pie\",\n      \"xpath\": \"category\",\n      \"dataSource\": [\n        { \"xvalue\": \"January\", \"yvalue\": 5000 },\n        { \"xvalue\": \"February\", \"yvalue\": 6000 },\n        { \"xvalue\": \"March\", \"yvalue\": 7000 },\n      ],\n    }\n  ]\n}";

        //    if (this.BindingContext is ChartViewModel viewModel)
        //    {
        //        viewModel.PrintJson(response);
        //    }
        //}
    }

}
