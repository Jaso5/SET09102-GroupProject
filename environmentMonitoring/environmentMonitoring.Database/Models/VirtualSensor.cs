using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace environmentMonitoring.Database.Models;

[Table("users")]
[PrimaryKey(nameof(v_sensor_Id))]
public class VirtualSensor
{
     public int v_sensor_Id { get; set; }
    [Required]
    public string catergory { get; set; }
    [Required]
    public string reference { get; set; }
    [Required]
    public float sensor_type { get; set; }
    [Required]
    public float url { get; set; }

    public int r_sensor_Id { get; set; }
    [ForeignKey("r_sensor_Id")]
    public RealSensor RealSensor { get; set; } = null!;

    public int quantity_id { get; set; }
    [ForeignKey("quantity_id")]
    public Quantities Quantity { get; set; } = null!;
    
}
