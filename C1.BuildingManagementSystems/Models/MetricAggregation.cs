using C1.BuildingManagementSystems.Contracts.Models.Enums;

namespace C1.BuildingManagementSystems.Contracts.Models
{
    public class MetricAggregation
    {
        public MetricEnums MetricEnum { get; set; }
        public int LastHourCount { get; set; }
        public decimal Average {  get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
    }
}
