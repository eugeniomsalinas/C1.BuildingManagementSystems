using C1.BuildingManagementSystems.Logging.Model;

namespace C1.BuildingManagementSystems.DataAccess.Interfaces
{
    public interface ILoggingRepository
    {
        Task<List<LogEntry>> GetLogsAsync();

        Task<LogEntry> LogMessageAsync(LogEntry log);

        Task<List<LogEntry>> GetRecentAlertsEntriesAsync();
    }
}
