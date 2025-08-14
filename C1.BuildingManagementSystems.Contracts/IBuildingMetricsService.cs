using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.Contracts.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace C1.BuildingManagementSystems.Contracts
{
    public interface IBuildingMetricsService
    {
        Task<IActionResult> Get(GetMetricsRequest request);

        Task<IActionResult> Push(PushMetricRequest request);
    }
}
