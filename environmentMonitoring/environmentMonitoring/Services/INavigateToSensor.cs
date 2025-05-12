using System;

namespace environmentMonitoring.Services;

public interface INavigateToSensor
{
    public Task NavigateToSensor(double? lon, double? lat);
}
