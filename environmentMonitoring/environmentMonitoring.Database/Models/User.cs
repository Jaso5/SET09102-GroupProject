using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace environmentMonitoring.Database.Models;

[Table("Users")]
[PrimaryKey(nameof(user_Id))]
public class User
{
    public int user_Id { get; set; }
    [Required]
    public string first_name { get; set; }
    [Required]
    public string surname { get; set; }
    [Required]
    [EmailAddress]
    public string email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string password { get; set; }

    // foreign key to Role table
    public int role_Id { get; set; }
    [ForeignKey("role_Id")]
    public Role Role { get; set; } = null!;

}