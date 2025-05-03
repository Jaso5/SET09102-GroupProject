using environmentMonitoring.Database.Models;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace environmentMonitoring.Services;

public class DataSeeder
{
    private static string password = BCrypt.Net.BCrypt.HashPassword("secret");
    private static string password2 = BCrypt.Net.BCrypt.HashPassword("supersecret");
    private static string password3 = BCrypt.Net.BCrypt.HashPassword("supersupersecret");

    public static void SeedData(EnvironmentAppDbContext context)
    {

        var users = new List<User>
            {
                new User {
                    first_name = "John",
                    surname = "Doe",
                    email = "john@example.com",
                    password = password,
                    role_Id = 1
                },
                new User {
                    first_name = "Jane",
                    surname = "Smith",
                    email = "jane@example.com",
                    password = password2,
                    role_Id = 2
                },
                new User {
                    first_name = "Peter",
                    surname = "Doe",
                    email = "peter@example.com",
                    password = password3,
                    role_Id = 3
                },
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        
    }

   

}
