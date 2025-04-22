using System;
using environmentMonitoring.ViewModels;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using Microsoft.EntityFrameworkCore;

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

    public RolePermissions GetRolePermissionById(int roleId, int permissionId)
    {
        try {
            return _context.RolePermissions.Single(r => r.role_Id == roleId && r.permission_Id == permissionId);
        } catch (Exception) {
            throw new Exception("Error retrieving role permission");
        }
    }

    /*! AddPermission method add a new column to the RolePermissions table
     *  giving the role the permission
     *  @param Takes a RolePermission object as a parameter 
     *  @throws Exception if there is an issue when trying to update teh database
     */
    public void AddPermission(RolePermissions permission) {
        try {
            _context.RolePermissions.Add(permission);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error adding permission");
        }
    }

    /*! RemovePermission method removes a column from the RolePermissions table
     *  removing the permission from the role
     *  @param Takes a RolePermission object as a parameter 
     *  @throws Exception if there is an issue when trying to update teh database
     */
    public void RemovePermission(RolePermissions permission) {
        try {
            _context.RolePermissions.Remove(permission);
            _context.SaveChanges();
        } catch (Exception) {
            throw new Exception("Error removing permission");
        }
    }

    /*! GetPermissionsList method retrieves a list of all permissions in the database 
     *  @throws Exception if there is an issue during retrieval
     *  @return Returns a list of permissions
     */
    public List<Permission> GetPermissionsList()
    {
        try {
            return _context.Permissions.ToList();
        } catch (Exception) {
            throw new Exception("Error retrieving permissions list");
        }
    }

    /*! GetRolesCurrentPermissions method retrieves a list of all current permissions a specified role has
     *  @param Takes a role ID as a parameter
     *  @throws Exception if there is an issue during retrieval
     *  @return Returns a list of current permissions for the specified role
     */
    public List<RolePermissions> GetRolesCurrentPermissions(int role_Id) {
        try {
            return _context.RolePermissions
            .Include(r => r.Permissions)
            .Where(r => r.role_Id == role_Id)
            .ToList();
        } catch (Exception) {
            throw new Exception("Error retrieving role permissions");
        }
    }

    /*! RoleHasPermission method queries the database for a match
     *  @throws Exception if there is an issue during the check
     *  @return Returns true if the role has the permission, false otherwise
     */
    public bool RoleHasPermission(int roleId, int permission_id)
    {   
        try {
            return _context.RolePermissions.Any(r => r.permission_Id == permission_id && r.role_Id == roleId);
        } catch (Exception) {
            throw new Exception("Error checking role permissions");
        }
    }

}
