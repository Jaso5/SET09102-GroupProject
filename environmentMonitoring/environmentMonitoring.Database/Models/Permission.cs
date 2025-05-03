using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("Permission")]
[PrimaryKey(nameof(permission_Id))]
public class Permission
{
    public int permission_Id { get; set;}
    public string? name { get; set; }
    public string? description { get; set; }

    public List<RolePermissions> RolePermissions { get; set; }  = new List<RolePermissions>();
}
