using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("Anomalies")]
[PrimaryKey(nameof(anomaly_Id))]
public class Anomalies
{
     public int anomaly_Id { get; set; }
    
    [Required]
    public string message { get; set; }
    

    // foreign key to Role table
    public int reading_Id { get; set; }
    [ForeignKey("reading_Id")]
    public Readings Reading { get; set; }
}
