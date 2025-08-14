using C1.BuildingManagementSystems.Contracts.Models.Enums;

namespace C1.BuildingManagementSystems.Contracts.Models.Requests
{
    public  class PushMetricRequest : Request
    {
        public string SensorId { get; set; }
        public MetricEnums MetricEnums { get; set; }
        public double MetricValue { get; set; } = 0;
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
    }
}
