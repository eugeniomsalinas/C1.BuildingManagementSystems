using C1.BuildingManagementSystems.Contracts.Models;
using C1.BuildingManagementSystems.Contracts.Models.Enums;
using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace C1.BuildingManagementSystems.DataAccess
{
    public class MetricEntriesRepository : IMetricEntriesRepository
    {
        private BmsDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly int _maxGetRecords;
        public MetricEntriesRepository(BmsDbContext bmsDbContext, IConfiguration configuration)
        {
            _dbContext = bmsDbContext;
            _configuration = configuration;
            _maxGetRecords = _configuration.GetValue<int>("AppConfigurations:GetMetricRecordMax");
        }

        public async Task<List<MetricEntry>> GetMetricEntriesAsync(MetricEnums MetricType)
        {
            return await _dbContext.MetricEntries.Where(m => m.MetricType == MetricType && m.EntryDateTime >= DateTime.UtcNow.AddHours(-1)).OrderByDescending(e => e.MetricEntryId).Take(_maxGetRecords).ToListAsync();
        }

        public async Task<MetricEntry> PushMetricEntryAsync(PushMetricRequest PushMetricRequest)
        {
            var newMetric = new MetricEntry()
            {
                SensorId = PushMetricRequest.SensorId,
                MetricType = PushMetricRequest.MetricEnum,
                MetricValue = PushMetricRequest.MetricValue,
                EntryDateTime = PushMetricRequest.DateTime
            };

            int id = 0;

            _dbContext.MetricEntries.Add(newMetric);
            _dbContext.SaveChanges();
            
            return newMetric;
        }
    }
}
