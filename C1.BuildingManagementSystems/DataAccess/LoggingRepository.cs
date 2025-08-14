using C1.BuildingManagementSystems.Contracts.Models;
using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.DataAccess.Interfaces;
using C1.BuildingManagementSystems.Logging.Model;
using Microsoft.EntityFrameworkCore;

namespace C1.BuildingManagementSystems.DataAccess
{
    public class LoggingRepository : ILoggingRepository
    {
        private BmsDbContext _dbContext;
        public LoggingRepository(BmsDbContext bmsDbContext)
        {
            _dbContext = bmsDbContext;
        }

        public async Task<List<LogEntry>> GetLogsAsync()
        {
            return await _dbContext.LogEntries.Take(50).ToListAsync();
        }
        public async Task<LogEntry> LogMessageAsync(LogEntry log)
        {
            _dbContext.LogEntries.Add(log);
            _dbContext.SaveChanges();

            return log;
        }
    }
}
