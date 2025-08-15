using C1.BuildingManagementSystems.DataAccess.Interfaces;
using C1.BuildingManagementSystems.Logging.Model;
using Microsoft.EntityFrameworkCore;

namespace C1.BuildingManagementSystems.DataAccess
{
    public class LoggingRepository : ILoggingRepository
    {
        private BmsDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly int _maxGetRecords;
        public LoggingRepository(BmsDbContext bmsDbContext, IConfiguration configuration)
        {
            _dbContext = bmsDbContext;
            _configuration = configuration;
            _maxGetRecords = _configuration.GetValue<int>("AppConfigurations:GetLogRecordMax");
        }

        public async Task<List<LogEntry>> GetLogsAsync()
        {
            return await _dbContext.LogEntries.OrderByDescending(e => e.LogEntryId).Take(_maxGetRecords).ToListAsync();
        }

        public async Task<List<LogEntry>> GetRecentAlertsEntriesAsync()
        {
            return await _dbContext.LogEntries.Where(a => a.Type == "ALERT" && a.EntryDate >= DateTime.UtcNow.AddHours(-1)).OrderByDescending(e => e.LogEntryId).Take(_maxGetRecords).ToListAsync();
        }

        public async Task<LogEntry> LogMessageAsync(LogEntry log)
        {
            _dbContext.LogEntries.Add(log);
            _dbContext.SaveChanges();

            return log;
        }
    }
}
