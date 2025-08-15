using C1.BuildingManagementSystems.Contracts.Models.Enums;

namespace C1.BuildingManagementSystems.Contracts.Models.Requests
{
    public  class PushMetricRequest
    {
        public string SensorId { get; set; }
        public MetricEnums MetricEnum { get; set; }
        public decimal MetricValue { get; set; } = 0;
        public string? Note { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
