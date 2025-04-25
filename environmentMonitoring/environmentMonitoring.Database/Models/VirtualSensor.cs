using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace environmentMonitoring.Database.Models;

[Table("VirtualSensors")]
[PrimaryKey(nameof(v_sensor_Id))]
public class VirtualSensor
{
     public int v_sensor_Id { get; set; }
    [Required]
    public string catergory { get; set; }
    
    public string reference { get; set; }
    [Required]
    public string sensor_type { get; set; }
    
    public float url { get; set; }

    public int r_sensor_Id { get; set; }
    [ForeignKey("r_sensor_Id")]
    public RealSensor RealSensor { get; set; } = null!;

    public int quantity_id { get; set; }
    [ForeignKey("quantity_id")]
    public Quantities Quantity { get; set; } = null!;

    public List<Readings> Readings { get; set; }  = new List<Readings>();

    public List<Reports> Reports { get; set; }  = new List<Reports>();
    
}
