using C1.BuildingManagementSystems.Contracts.Models.Enums;

namespace C1.BuildingManagementSystems.Contracts.Models
{
    public class MetricEntry
    {
        public int MetricEntryId { get; set; }
        public string SensorId { get; set; }
        public MetricEnums MetricType { get; set; }
        public double MetricValue {  get; set; }
        public DateTime EntryDateTime { get; set; }
    }
}
