using C1.BuildingManagementSystems.Contracts.Models.Enums;

namespace C1.BuildingManagementSystems.Contracts.Models.Requests
{
    public class GetMetricsRequest : Request
    {
        public int NumberofRecords { get; set; } = 20;
        public string SensorId { get; set; }
        public MetricEnums? MetricEnums { get; set; } = null;
    }
}
