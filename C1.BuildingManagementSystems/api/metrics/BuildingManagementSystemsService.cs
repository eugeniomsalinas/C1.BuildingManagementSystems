using C1.BuildingManagementSystems.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace C1.BuildingManagementSystems.api.metrics
{
    [Route("api/metrics/[controller]")]
    [ApiController]
    public class BuildingMetricsService : ControllerBase, IBuildingMetricsService
    {
        public BuildingMetricsService()
        {

        }

        [HttpGet]
        [Route("GetMetrics")]
        public async Task<string> Get(string request)
        {
            return "Hello World";
        }

        [HttpPost]
        [Route("PushMetric")]
        public async Task<string> Push(string request)
        {
            return "Hello World";
        }
    }
}
