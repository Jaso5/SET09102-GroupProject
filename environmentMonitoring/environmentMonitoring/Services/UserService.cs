using System;
using environmentMonitoring.Database.Models;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Diagnostics;


 using System.Diagnostics;

namespace environmentMonitoring.Services;

public class UserService: IReadDataService, IUpdateDataService, IValidationService
{

    private EnvironmentAppDbContext _context;

    public UserService(EnvironmentAppDbContext context)
    {
        _context = context;
    }

    public List<User> GetUserList()
    {
        try {
        return _context.Users
            .Include(u => u.Role)
            .ToList();
        } catch (Exception) {
            Debug.WriteLine("Didn't throw exception");
            throw new Exception("Error retrieving user list");
        }
    }
    public async Task<User?> CredentialsCheck(String email, string password)
    {
         var user = await _context.Users
        .Include(u => u.Role)
        .ThenInclude(r => r.RolePermissions)
        .ThenInclude(rp => rp.Permissions)
        .FirstOrDefaultAsync(u => u.email == email);

         if (user != null || !BCrypt.Net.BCrypt.Verify(password, user.password)) 
         {

            return user;
            
         }

         return null;
        
    }




}
