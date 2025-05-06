using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace environmentMonitoring.PartialViews;

public partial class VirtualSensorView : ContentView
{
    private readonly VirtualSensor vs;
    private readonly EnvironmentAppDbContext dbctx;

    public VirtualSensorView(VirtualSensor vs, EnvironmentAppDbContext dbctx)
    {
        this.vs = vs;
        this.dbctx = dbctx;
        InitializeComponent();

        this.VSSettings.Command = new AsyncRelayCommand(this.NavToSettings);
        Quantity.Text = vs.Quantity.quantity;

        var data = ReadingsToSeries(getReadings(vs.v_sensor_Id), vs.Quantity.quantity);
        Chart.Series = data;
        Chart.XAxes = new List<Axis>
        {
            new Axis
            {
                Labeler = value => new DateTime((long) value).ToString("MMMM dd hh:mm:ss"),
                LabelsRotation = 15
            }
        };
    }

    /// <summary>
    /// Gets all readings from a sensor of given id
    /// </summary>
    /// <param name="id">Id of sensor</param>
    /// <returns></returns>
    private List<Readings> getReadings(int id)
    {
        return (from reading in dbctx.Readings
                where reading.v_sensor_id == id
                select reading).ToList();
    }

    /// <summary>
    /// Convert a list of readings to a DateTimePoint based ISeries for Livecharts
    /// </summary>
    /// <param name="readings">List of readings to be converted</param>
    /// <returns></returns>
    public static ISeries[] ReadingsToSeries(List<Readings> readings, string name)
    {
        var series = new ISeries[]
        {
            new LineSeries<DateTimePoint>
            {
                Values = readings.Select(r => new DateTimePoint(r.timestamp, r.value)).ToList(),
                Name = name,
            }
        };

        return series;
    }

    /// <summary>
    /// Navigate to the settings page for the current virtualSensor
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task NavToSettings()
    {
        await Shell.Current.GoToAsync(
            nameof(Views.VirtualSensorSettings),
            new Dictionary<string, object>
                {
                    { "virtualSensor", this.vs }
                }
            );
    }
}