using environmentMonitoring.Database.Models;

namespace environmentMonitoring.ViewModels;

public class SensorDisplayModel
{
    public int r_sensor_Id { get; set; }
    public float status { get; set; }

    public string StatusText => IsActive ? "Active" : "Not Active";
    public bool IsInactive => !IsActive;
    public bool IsActive => status != -1f;

    public SensorDisplayModel(RealSensor sensor)
    {
        r_sensor_Id = sensor.r_sensor_Id;
        status = sensor.status;
    }
}




