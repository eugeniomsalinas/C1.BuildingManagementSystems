namespace C1.BuildingManagementSystems.Contracts
{
    public interface IBuildingMetricsService
    {
        Task<string> Get(string request);

        Task<string> Push(string request);
    }
}
