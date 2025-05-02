namespace environmentMonitoring.Services
{
    public class SensorDataService
    {
        public async Task<List<TrendData>> GetEnvironmentalTrendDataAsync()
        {
            // Example data, replace with actual logic to fetch data
            await Task.Delay(1000); // Simulating a data fetch delay
            return new List<TrendData>
            {
                new TrendData { SensorId = 1, Value = 22.5, Timestamp = DateTime.Now.AddMinutes(-10) },
                new TrendData { SensorId = 2, Value = 18.3, Timestamp = DateTime.Now.AddMinutes(-5) },
                new TrendData { SensorId = 3, Value = 20.1, Timestamp = DateTime.Now }
            };
        }
    }

    public class TrendData
    {
        public int SensorId { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
