using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("roles")]
[PrimaryKey(nameof(role_Id))]
public class Role
{
     public int role_Id { get; set; }
    [Required]
    public string role_type { get; set; }
    [Required]
    public string permission { get; set; }

}