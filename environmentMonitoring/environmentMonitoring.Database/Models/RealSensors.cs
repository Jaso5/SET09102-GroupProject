using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("RealSensors")]
[PrimaryKey(nameof(r_sensor_Id))]
public class RealSensor
{
     public int r_sensor_Id { get; set; }
    [Required]
    public float lat { get; set; }
    [Required]
    public float lon { get; set; }
    [Required]
    public float frequency { get; set; }

    public List<VirtualSensor> VirtualSensor { get; set; }  = new List<VirtualSensor>();
}
