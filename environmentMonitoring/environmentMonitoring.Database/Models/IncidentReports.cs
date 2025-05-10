using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("IncidentReports")]
[PrimaryKey(nameof(incident_Id))]
public class IncidentReports
{

     public int incident_Id { get; set; }
    
    [Required]
    public string type { get; set; }
    [Required]
    public string status { get; set; }
    [Required]
    public DateTime? reportDate { get; set; }

    public DateTime? lastUpdatedDate { get; set; }

    public string? description { get; set; }

    public string? next_steps { get; set; }

    public string? resolution { get; set; }

    // foreign keys
    public int? reading_Id { get; set; }
    [ForeignKey("reading_Id")]
    public Readings? Reading { get; set; }

     public int? r_sensor_Id { get; set; }
    [ForeignKey("reading_Id")]
    public RealSensor? RealSensor { get; set; }
}
