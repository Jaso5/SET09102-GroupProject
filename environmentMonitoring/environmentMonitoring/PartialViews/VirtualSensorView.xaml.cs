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
    //private readonly List<Readings> readings;

    public VirtualSensorView(VirtualSensor vs, EnvironmentAppDbContext dbctx)
    {
        this.vs = vs;
        this.dbctx = dbctx;
        InitializeComponent();

        Quantity.Text = vs.Quantity.quantity;

        var data = ReadingsToSeries(getReadings(vs.v_sensor_Id));
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

    private List<Readings> getReadings(int id)
    {
        return (from reading in dbctx.Readings
                where reading.v_sensor_id == id
                select reading).ToList();
    }

    private ISeries[] ReadingsToSeries(List<Readings> readings)
    {
        var series = new ISeries[]
        {
            new LineSeries<DateTimePoint>
            {
                Values = readings.Select(r => new DateTimePoint(r.timestamp, r.value)).ToList(),
                Name = vs.Quantity.quantity,
            }
        };

        return series;
    }
}