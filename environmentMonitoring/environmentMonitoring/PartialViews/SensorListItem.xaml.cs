using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
using Microsoft.Maui.Layouts;
using System.Windows.Input;

namespace environmentMonitoring.PartialViews;

public partial class SensorListItem : ContentView
{
    private RealSensor rs;

    public SensorListItem(RealSensor rs)
	{
        this.rs = rs;
		InitializeComponent();
        this.NavButton.Command = new AsyncRelayCommand(this.NavToSensorPage);

        // Populate info from the VirtualSensor into the fields
        Category.Text = rs.VirtualSensor.First().catergory;
        Units.Text = String.Join(", ", rs.VirtualSensor.Select(vs => vs.Quantity.symbol));
	}

    /// <summary>
    /// Navigate to the sensor page, passing the current RealSensor
    /// </summary>
    /// <returns></returns>
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