using C1.BuildingManagementSystems.Contracts.Models;
using C1.BuildingManagementSystems.Contracts.Models.Enums;
using C1.BuildingManagementSystems.Contracts.Models.Requests;

namespace C1.BuildingManagementSystems.DataAccess.Interfaces
{
    public interface IMetricEntriesRepository
    {
        Task<List<MetricEntry>> GetMetricEntriesAsync(MetricEnums MetricType);

        Task<MetricEntry> PushMetricEntryAsync(PushMetricRequest pushMetricRequest);
    }
}
