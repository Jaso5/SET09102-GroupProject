using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("Quantities")]
[PrimaryKey(nameof(quantity_Id))]
public class Quantities
{
    public int quantity_Id { get; set; }
    [Required]
    public string quantity { get; set; }
    [Required]
    public string symbol { get; set; }
    [Required]
    public string unit { get; set; }
    [Required]
    public string description { get; set; }
    [Required]
    public float safe_level { get; set; }
    [Required]
    public float min_threshold { get; set; }
    [Required]
    public float max_threshold { get; set; }

    public List<VirtualSensor> VirtualSensors { get; set; }  = new List<VirtualSensor>();
}
