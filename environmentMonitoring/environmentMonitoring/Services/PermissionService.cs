using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace environmentMonitoring.Services
{
    public class PermissionService
    {
        private readonly EnvironmentAppDbContext _dbContext;

        public PermissionService(EnvironmentAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Check if the current user has the required permission
        public async Task<bool> HasPermissionAsync(int permissionId, int roleId)
        {
            // Check if the role has the specific permission
            var permissionExists = await _dbContext.RolePermissions
                .AnyAsync(rp => rp.role_Id == roleId && rp.permission_Id == permissionId);

            return permissionExists;
        }
    }
}
