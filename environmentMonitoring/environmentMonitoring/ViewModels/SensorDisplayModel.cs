using environmentMonitoring.Database.Models;
using System;

namespace environmentMonitoring.ViewModels;

public class SensorDisplayModel
{
    public int r_sensor_Id { get; set; }
    public float status { get; set; }

    public string StatusText => IsActive ? "Active" : "Not Active";
    public bool IsInactive => !IsActive;
    public bool IsActive => status != -1f;

    // New properties to store maintenance date
    public DateTime? MaintenanceDate { get; set; }  // Nullable DateTime for the selected date
    public DateTime MaintenanceMinDate => DateTime.Now; // Minimum date to be today

    public SensorDisplayModel(RealSensor sensor)
    {
        r_sensor_Id = sensor.r_sensor_Id;
        status = sensor.status;
    }
}





