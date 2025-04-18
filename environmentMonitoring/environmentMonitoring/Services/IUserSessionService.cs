using System;

namespace environmentMonitoring.Services;

public interface IUserSessionService
{
    public int? userId { get; set; }
    public string? role { get; set; }
    public string? username { get; set; }
    public List<string?> permissions { get; set; }

    void setUserSession(int userId, string role, string username, List<string?> permissions);
    
    bool hasPermission(string permissionName);
    
    void clearUserSession();
    

}
