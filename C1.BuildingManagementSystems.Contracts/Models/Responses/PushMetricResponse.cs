namespace C1.BuildingManagementSystems.Contracts.Models.Responses
{
    public class PushMetricResponse : Response
    {
        public long MetricEntryId { get; set; }
        public bool AlertTriggered { get ; set; } = false;
    }
}
