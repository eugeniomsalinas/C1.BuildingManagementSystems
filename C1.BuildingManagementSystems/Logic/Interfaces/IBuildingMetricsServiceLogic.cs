using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.Contracts.Models.Responses;

namespace C1.BuildingManagementSystems.Logic.Interfaces
{
    public interface IBuildingMetricsServiceLogic
    {
        Task<GetMetricsResponse> GetRecentMetricEntries();

        Task<PushMetricResponse> PushMetricEntry(PushMetricRequest Request);
    }
}
