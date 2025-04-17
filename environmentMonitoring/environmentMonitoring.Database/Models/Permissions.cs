using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("Permissions")]
[PrimaryKey(nameof(permission_Id))]
public class Permissions
{
    public int permission_Id { get; set;}
    public string? name { get; set; }
    public string? description { get; set; }

    public List<Role> Role { get; set; }  = new List<Role>();
}
