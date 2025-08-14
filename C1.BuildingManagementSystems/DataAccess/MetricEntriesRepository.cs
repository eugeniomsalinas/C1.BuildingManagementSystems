using C1.BuildingManagementSystems.Contracts.Models;
using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace C1.BuildingManagementSystems.DataAccess
{
    public class MetricEntriesRepository : IMetricEntriesRepository
    {
        private BmsDbContext _dbContext;
        public MetricEntriesRepository(BmsDbContext bmsDbContext) 
        {
            _dbContext = bmsDbContext;
        }

        public async Task<List<MetricEntry>> GetMetricEntriesAsync()
        {
            return await _dbContext.MetricEntries.Take(20).ToListAsync();
        }

        public async Task<MetricEntry> PushMetricEntryAsync(PushMetricRequest pushMetricRequest)
        {
            var newMetric = new MetricEntry()
            {
                SensorId = pushMetricRequest.SensorId,
                MetricType = pushMetricRequest.MetricEnums,
                MetricValue = pushMetricRequest.MetricValue,
                EntryDateTime = pushMetricRequest.DateTime
            };

            int id = 0;

            _dbContext.MetricEntries.Add(newMetric);
            _dbContext.SaveChanges();
            
            return newMetric;
        }
    }
}
