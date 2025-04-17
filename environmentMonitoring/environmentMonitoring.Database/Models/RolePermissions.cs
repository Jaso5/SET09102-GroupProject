using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace environmentMonitoring.Database.Models;

[Table("RolePermissions")]
public class RolePermissions
{
    public int role_Id { get; set; }
    [ForeignKey("role_Id")]
    public Role Role { get; set; } = null!;

    public int permission_Id { get; set; }
    [ForeignKey("permission_Id")]
    public Permission Permissions { get; set; } = null!;


}
