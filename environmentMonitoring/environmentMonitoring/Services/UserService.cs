using System;
using environmentMonitoring.Database.Models;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


 using System.Diagnostics;

namespace environmentMonitoring.Services;

public class UserService: IReadDataService, IUpdateDataService, IValidationService
{

    private EnvironmentAppDbContext _context;

    public UserService(EnvironmentAppDbContext context)
    {
        _context = context;
    }
    public async Task<User?> CredentialsCheck(String email, string password)
    {
         var user = await _context.Users
        .Include(u => u.Role)
        .ThenInclude(r => r.RolePermissions)
        .ThenInclude(rp => rp.Permissions)
        .FirstOrDefaultAsync(u => u.email == email);

        bool passwordVerified = BCrypt.Net.BCrypt.Verify(password, user.password);

         if (user != null && passwordVerified) 
         {

            return user;
            
         }

         return null;
        
    }




}
