using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("Reports")]
[PrimaryKey(nameof(report_Id))]
public class Reports
{
     public int report_Id { get; set; }
    [Required]
    public string title { get; set; }
    [Required]
    public string body { get; set; }
    [Required]
    public string trend { get; set; }

     public int v_sensor_id { get; set; }
    [ForeignKey("v_sensor_id")]
    public VirtualSensor VirtualSensor { get; set; } = null!;
}
