using C1.BuildingManagementSystems.Contracts.Models.Enums;
using C1.BuildingManagementSystems.Contracts.Models.Requests;
using C1.BuildingManagementSystems.Contracts.Models.Responses;
using C1.BuildingManagementSystems.DataAccess.Interfaces;
using C1.BuildingManagementSystems.Logging.Model;
using C1.BuildingManagementSystems.Logic.Interfaces;

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

            try
            {
                var result = await _metricEntriesRepository.GetMetricEntriesAsync();
                getMetricsResponse.Metrics = result;

                // TODO return a summary report

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

            // CHECK IF METRIC FOR GIVEN TYPE IS OUT OF RANGE
            // TODO Extract this logic into it's own methods later
            if (Request.MetricEnums == MetricEnums.Temperature && (Request.MetricValue > 50 ||  Request.MetricValue < 35))
            {
                var newLog = new LogEntry()
                {
                    Message = Request.MetricValue > 50 ? "Temperature is too high!" : "Temperature is too low!",
                    EntryDate = Request.DateTime,
                    Type = "ALERT"
                };

                await _loggingRepository.LogMessageAsync(newLog);

                response.AlertTriggered = true;
            }

            var result = await _metricEntriesRepository.PushMetricEntryAsync(Request);

            response.MetricEntryId = result.MetricEntryId;

            return response;
        }
    }
}
