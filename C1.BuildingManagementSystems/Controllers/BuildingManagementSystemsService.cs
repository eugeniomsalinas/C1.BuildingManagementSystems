using C1.BuildingManagementSystems.Contracts.Models.Enums;
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

            return Ok(result.MetricsSummary != null ? result.MetricsSummary : result.Message);
        }

        [HttpPost]
        [Route("PushMetric")]
        public async Task<IActionResult> Push(PushMetricRequest request)
        {
            try
            {
                ValidatePushMetricRequest(request);

                var result = await _buildingMetricsServiceLogic.PushMetricEntry(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetRecentAlerts")]
        public async Task<IActionResult> GetRecentAlerts()
        {
            var result = await _buildingMetricsServiceLogic.GetRecentAlertsEntries();

            return Ok(result);
        }

        private static void ValidatePushMetricRequest(PushMetricRequest request)
        {
            if (string.IsNullOrEmpty(request.SensorId))
                throw new Exception("SensorId is required.");

            bool isValidEnumVal = Enum.IsDefined(typeof(MetricEnums), request.MetricEnum);

            if (!isValidEnumVal)
                throw new Exception("MetricEnums value is not valid.");

            if(request.DateTime == DateTime.MinValue)
                throw new Exception("The DateTime is not valid.");

            if(request.DateTime == null)
            {
                request.DateTime = DateTime.UtcNow;
            }
        }
    }
}
