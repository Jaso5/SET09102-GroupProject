
namespace environmentMonitoring.Services;

public interface IAuthService
{
    public bool IsAdmin();
    public bool HasPermission();

}
