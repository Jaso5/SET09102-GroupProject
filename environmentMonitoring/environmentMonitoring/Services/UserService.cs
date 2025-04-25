using environmentMonitoring.Database.Models;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


namespace environmentMonitoring.Services;

/*! UserService is responsible for handling user specific services, from validation 
     *  to retrieving and updating users information.
     */

/*! UserService is responsible for handling user specific services, from validation 
     *  to retrieving and updating users information.
     */

public class UserService: IReadDataService, IUpdateDataService, IValidationService
{

    private EnvironmentAppDbContext _context;

    public UserService(EnvironmentAppDbContext context)
    {
        _context = context;
    }

    /*! GetUserById method retrieves a single user by ID
     *  @param Takes a user ID as a parameter
     *  @throws Exception if there is an issue during retrieval
     *  @return Returns the user 
     */
    public User GetUserById(int userId) {
        try {
            return _context.Users.Single(u => u.user_Id == userId);
        } catch (Exception) {
            throw new Exception("Error retrieving user");
        }
    }

    /*! UpdateUserRole method updates a user role
     *  @param Takes a user object as a parameter
     *  @throws Exception if there is an issue during the update
     */
    public void UpdateUserRole(User user)
    {
        try {
            _context.Users.Update(user);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error updating users role");
        }
    }


    /*! GetUserList method retrieves a list of all users
     *  @throws Exception if there is an issue during retrieval
     *  @return Returns a list of all users
     */
    public List<User> GetUserList()
    {
        try {
        return _context.Users
            .Include(u => u.Role)
            .ToList();
        } catch (Exception) {
            throw new Exception("Error retrieving user list");
        }
    }


    public async Task<User?> CredentialsCheck(String email, string password)
    {
         var user = await _context.Users
        .Include(u => u.Role)
        .FirstOrDefaultAsync(u => u.email == email);

         if (user != null || !BCrypt.Net.BCrypt.Verify(password, user.password)) 
         {
            return user;
         }
         return null;
        
    }




}
