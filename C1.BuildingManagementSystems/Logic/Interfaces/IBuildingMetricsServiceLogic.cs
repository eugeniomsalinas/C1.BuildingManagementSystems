using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.Contracts.Models.Responses;
using C1.BuildingManagementSystems.Logging.Model;

namespace C1.BuildingManagementSystems.Logic.Interfaces
{
    public interface IBuildingMetricsServiceLogic
    {
        Task<GetMetricsResponse> GetRecentMetricEntries();

        Task<PushMetricResponse> PushMetricEntry(PushMetricRequest Request);

        Task<List<LogEntry>> GetRecentAlertsEntries();
    }
}
