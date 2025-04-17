using System;
using environmentMonitoring.Database.Models;

namespace environmentMonitoring.Services;

public interface IValidationService
{
    Task<User?> CredentialsCheck(String email, string password);
}
