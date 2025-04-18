using System;

namespace environmentMonitoring.Services;

public class UserSessionService : IUserSessionService
{
    public int? userId { get; set; }
    public string? role { get; set; }
    public string? username { get; set; }
    public List<string?> permissions { get; set; }

    public void setUserSession(int userId, string role, string username, List<string?> permissions)
    {
        this.userId = userId;
        this.role = role;
        this.username = username;
        this.permissions = permissions;
    }

    public bool hasPermission(string permissionName)
    {
        return permissions.Contains(permissionName);
    }

    public void clearUserSession()
    {
        userId = null;
        role = null;
        username = null;
        permissions = new List<string?>();
    }
}
