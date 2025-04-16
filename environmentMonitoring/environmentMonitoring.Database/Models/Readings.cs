using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("Readings")]
[PrimaryKey(nameof(reading_Id))]
public class Readings
{
    public int reading_Id { get; set; }
    [Required]
    public DateTime timestamp { get; set; }
    [Required]
    public float value { get; set; }

     // foreign key to Role table
    public int v_sensor_id { get; set; }
    [ForeignKey("v_sensor_id")]
    public VirtualSensor VirtualSensor { get; set; } = null!;

    public List<Anomalies> Anomalies { get; set; } = new List<Anomalies>();
}
