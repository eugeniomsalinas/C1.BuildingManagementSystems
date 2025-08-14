namespace C1.BuildingManagementSystems.Contracts.Models.Responses
{
    public class GetMetricsResponse : Response
    {
        public List<MetricEntry> Metrics { get; set; }
    }
}
