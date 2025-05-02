using environmentMonitoring.Database.Data;
using environmentMonitoring.PartialViews;
using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class SensorListPage : ContentPage
{

	public SensorListPage(SensorListViewModel vm)
	{
		this.BindingContext = vm;
        InitializeComponent();

		vm.RealSensors()
			.ForEach(rs => Body.Add(new SensorListItem(rs)));
    }
}
