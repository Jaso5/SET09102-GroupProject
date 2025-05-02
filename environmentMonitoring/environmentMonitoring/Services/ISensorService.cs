// ISensorService.cs
using environmentMonitoring.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace environmentMonitoring.Services
{
    public interface ISensorService
    {
        Task<List<RealSensor>> GetAllSensorsAsync();  // Change 'Sensor' to 'RealSensor'
    }
}
