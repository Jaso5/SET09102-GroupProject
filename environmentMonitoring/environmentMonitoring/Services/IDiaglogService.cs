using System;

namespace environmentMonitoring.Services;

public interface IDiaglogService
{
    Task ShowAlertAsync(string title, string message, string buttonText);
}
