using System;
using environmentMonitoring.ViewModels;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using Microsoft.IdentityModel.Tokens;

namespace environmentMonitoring.Services;

/*! Role Permission service class performs CRUD operations on roles and permissions
 *
 */
public class RolePermissionService
{
    private readonly EnvironmentAppDbContext _context;

    public RolePermissionService(EnvironmentAppDbContext context)
    {
        _context = context;
    }


    /*! CreateRole method adds a new role to the database
     *  @param Takes a role as a parameter 
     *  @throws Exception if there is an error when attempting to add it to the database
     */
    public void CreateRole(Role role)
    {
        try {
            _context.Roles.Add(role);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error creating role");
        }
    }

    /*! GetRoleById method retrieves a role from the database by it's ID
     *  @param Takes an id as a parameter 
     *  @throws Exception if there is an issue when attempting to retrieve it from the database
     *  @return Returns the role to the user
     */
    public Role GetRoleById(int roleId)
    {
        try {
            return  _context.Roles.Single(r => r.role_Id == roleId);
        } catch (Exception) {
            throw new Exception("Error retrieving role");
        }
    }

    /*! UpdateRole method updates a role in the database
     *  @param Takes a role as a parameter 
     *  @throws Exception if there is an issue when trying to update the role
     */
    public void UpdateRole(Role role)
    {
        try {
            _context.Roles.Update(role);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error updating role");
        }
    }

    /*! DeleteRole method deletes a role from the database
     *  @param Takes a role as a parameter 
     *  @throws Exception if there is an issue when trying to delete the role
     */
    public void DeleteRole(Role role)
    {
        try {
            _context.Roles.Remove(role);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error deleting role");
        }
    }

    /*! ReloadRole method re-pulls a currently tracked role from the database to updates it's properties
     *  @param Takes a role as a parameter 
     *  @throws Exception if there is an issue when trying to reload the role
     */
    public void ReloadRole(Role role)
    {
        try {
            _context.Entry(role).Reload();
        } catch (Exception) {
            throw new Exception("Error reloading role");
        }
    }

    /*! RoleExists method checks to see if a role exists in the database
     *  @param Takes a role as a parameter 
     *  @throws Exception if there is an issue during checking
     *  @return Returns true if the role exists, false otherwise
     */
    public bool RoleExists(Role role)
    {
        return _context.Roles.Any(r => r.role_type == role.role_type && r.role_Id != role.role_Id);
    }

    /*! GetRoleList method retrieves a list of all roles in the database 
     *  @throws Exception if there is an issue during retrieval
     *  @return Returns a list of roles to the user
     */
    public List<Role> GetRoleList()
    {
        try {
            return _context.Roles.ToList();
        } catch (Exception) {
            throw new Exception("Error retrieving role list");
        }
    }




     

}
