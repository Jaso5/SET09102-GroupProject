using System;

namespace environmentMonitoring.Services;

public class DiaglogService : IDiaglogService
{
    public async Task ShowAlertAsync(string title, string message, string buttonText)
    {
        await Shell.Current.DisplayAlert(title, message, buttonText);
    }
}
