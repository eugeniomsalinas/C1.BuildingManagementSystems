using C1.BuildingManagementSystems.Contracts.Models;
using C1.BuildingManagementSystems.Contracts.Models.Enums;
using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.Contracts.Models.Responses;
using C1.BuildingManagementSystems.DataAccess.Interfaces;
using C1.BuildingManagementSystems.Logging.Model;
using C1.BuildingManagementSystems.Logic.Interfaces;
using System.Text.Json;

namespace C1.BuildingManagementSystems.Logic
{
    public class BuildingMetricsServiceLogic : IBuildingMetricsServiceLogic
    {
        private IMetricEntriesRepository _metricEntriesRepository;
        private ILoggingRepository _loggingRepository;
        public BuildingMetricsServiceLogic(IMetricEntriesRepository metricEntriesRepository, ILoggingRepository loggingRepository) 
        {
            _metricEntriesRepository = metricEntriesRepository;
            _loggingRepository = loggingRepository;
        }

        public async Task<GetMetricsResponse> GetRecentMetricEntries()
        {
            GetMetricsResponse getMetricsResponse = new GetMetricsResponse();
            List<MetricAggregation> metricsList = new List<MetricAggregation>();

            try
            {
                foreach(MetricEnums metricEnum in Enum.GetValues(typeof(MetricEnums)))
                {
                    var result = await _metricEntriesRepository.GetMetricEntriesAsync(metricEnum);

                    if(result.Count > 0)
                    {
                        MetricAggregation metricAgg = new MetricAggregation();
                        metricAgg.MetricEnum = metricEnum;
                        metricAgg.LastHourCount = result.Count();
                        metricAgg.Average = result.Average(x => x.MetricValue);
                        metricAgg.Max = result.Select(x => x.MetricValue).Max();
                        metricAgg.Min = result.Select(x => x.MetricValue).Min();

                        metricsList.Add(metricAgg);
                    }
                }

                getMetricsResponse.MetricsSummary = metricsList;

                if (getMetricsResponse.MetricsSummary == null || getMetricsResponse.MetricsSummary.Count == 0)
                {
                    getMetricsResponse.Message = "No metrics were reported within the last hour.";
                    getMetricsResponse.Success = true;
                }

                return getMetricsResponse;
            }
            catch (Exception ex) 
            {
                var newLog = new LogEntry()
                {
                    Message = "The most recent metric entires could not be retrieved.",
                    EntryDate = DateTime.UtcNow,
                    Data = ex.Data.ToString(),
                    Type = "ERROR",
                };

                await _loggingRepository.LogMessageAsync(newLog);
                getMetricsResponse.Success = false;
                getMetricsResponse.Message = newLog.Message;
            }
            return getMetricsResponse;
        }

        public async Task<PushMetricResponse> PushMetricEntry(PushMetricRequest Request)
        {
            PushMetricResponse response = new PushMetricResponse();

            response.AlertTriggered = await ThresholdCheck(Request);

            var result = await _metricEntriesRepository.PushMetricEntryAsync(Request);

            response.MetricEntryId = result.MetricEntryId;

            return response;
        }


        public async Task<List<LogEntry>> GetRecentAlertsEntries()
        {
            var result = await _loggingRepository.GetRecentAlertsEntriesAsync();
            return result;
        }
        
        private async Task<bool> ThresholdCheck(PushMetricRequest Request)
        {
            if (Request.MetricEnum == MetricEnums.Temperature && (Request.MetricValue > 86 || Request.MetricValue < 35))
            {
                var newLog = new LogEntry()
                {
                    Message = Request.MetricValue > 86 ? "Temperature is too high!" : "Temperature is too low!",
                    EntryDate = (DateTime)Request.DateTime,
                    Type = "ALERT",
                    Data = JsonSerializer.Serialize(Request)
                };

                await _loggingRepository.LogMessageAsync(newLog);

                return true;
            }

            if (Request.MetricEnum == MetricEnums.Humidity && (Request.MetricValue > 60 || Request.MetricValue < 30))
            {
                var newLog = new LogEntry()
                {
                    Message = Request.MetricValue > 60 ? "Humidity is too high!" : "Humidity is too low!",
                    EntryDate = (DateTime)Request.DateTime,
                    Type = "ALERT",
                    Data = JsonSerializer.Serialize(Request)
                };

                await _loggingRepository.LogMessageAsync(newLog);

                return true;
            }

            return false;
        }
    }
}
