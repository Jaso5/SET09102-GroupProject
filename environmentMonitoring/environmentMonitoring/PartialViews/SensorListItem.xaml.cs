using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
using Microsoft.Maui.Layouts;
using System.Windows.Input;

namespace environmentMonitoring.PartialViews;

public partial class SensorListItem : ContentView
{
    private RealSensor rs;
    //public ICommand ToSensorPage { get; }

    public SensorListItem(RealSensor rs)
	{
        this.rs = rs;
		InitializeComponent();
        this.NavButton.Command = new AsyncRelayCommand(this.NavToSensorPage);

        Category.Text = rs.VirtualSensor.First().catergory;
        Units.Text = String.Join(", ", rs.VirtualSensor.Select(vs => vs.Quantity.symbol));
	}

    [RelayCommand]
    private async Task NavToSensorPage()
    {
        await Shell.Current.GoToAsync(
            nameof(Views.SensorPage),
            new Dictionary<string, object>
                {
                    { "realSensor", this.rs }
                }
            );
    }
}