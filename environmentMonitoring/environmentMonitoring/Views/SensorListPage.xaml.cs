using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class SensorListPage : ContentPage
{
    // Inject the view model into the page constructor
    public SensorListPage(SensorListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;  
    }
}


