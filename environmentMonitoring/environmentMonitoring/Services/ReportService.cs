using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace environmentMonitoring.Services
{
    public class ReportService
    {
        private readonly EnvironmentAppDbContext _context;

        // Constructor injection for DB context
        public ReportService(EnvironmentAppDbContext context)
        {
            _context = context;
        }

        // Fetch all reports from the database
        public async Task<List<Reports>> GetReportsAsync()
        {
            return await _context.Reports.ToListAsync(); // Retrieves all reports from the database
        }
    }
}

