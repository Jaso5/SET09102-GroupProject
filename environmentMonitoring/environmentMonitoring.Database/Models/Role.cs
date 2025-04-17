using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("Roles")]
[PrimaryKey(nameof(role_Id))]
public class Role
{
     public int role_Id { get; set; }
    [Required]
    public string role_type { get; set; }

    public List<User> Users { get; set; }  = new List<User>();

     public List<RolePermissions> RolePermissions { get; set; }  = new List<RolePermissions>();

}