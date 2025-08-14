using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace C1.BuildingManagementSystems.Controllers
{
    [Route("api/metrics/[controller]")]
    [ApiController]
    public class BuildingMetricsService : ControllerBase
    {
        private readonly IBuildingMetricsServiceLogic _buildingMetricsServiceLogic;
        public BuildingMetricsService(IBuildingMetricsServiceLogic buildingMetricsServiceLogic)
        {
            _buildingMetricsServiceLogic = buildingMetricsServiceLogic;
        }

        [HttpGet]
        [Route("GetMetrics")]
        public async Task<IActionResult> Get()
        {
            var result = await _buildingMetricsServiceLogic.GetRecentMetricEntries();

            return Ok();
        }

        [HttpPost]
        [Route("PushMetric")]
        public async Task<IActionResult> Push(PushMetricRequest request)
        {
            var result = await _buildingMetricsServiceLogic.PushMetricEntry(request);

            return Ok();
        }
    }
}
