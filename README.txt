Building Managment Systems Service

Description: 
	The purpose of this service is to monitor environment metrics, such as Temperature, Humidity, Energy Use, etc., in real time. This service will be built out as needs arise to collect and provide new data points to the end user.

APIS

GetMetric
Description: 
	This GET endpoint gets the most recent Metric Entries that have been persisted into the MetricEntries table. The max number of records returned is configurable via appSettings.json ("AppConfigurations:GetMetricRecordMax").
	The response includes a summary of each metric type (Temperature, Humidity, etc.).
	
	https://localhost:7047/api/metrics/BuildingMetricsService/GetMetric

GetRecentAlerts
Description:
	This GET endpoint gets the most recent ALERT type log Entries that have been persisted into the LogEntries table. The max number of records returned is configurable via appSettings.json ("AppConfigurations:GetLogRecordMax").
	
	https://localhost:7047/api/metrics/BuildingMetricsService/GetRecentAlerts

PushMetric
Description:
	This POST endpoint recieves metric data that is persisted into the MetricEntries table. The data is processed through business rule logic that checks to ensure the values retrieved are within a given range. If out of range then an alert entry is made in the LogEntries table.

	https://localhost:7047/api/metrics/BuildingMetricsService/PushMetric

Sample Request:
{
  "user": "esalinas",
  "appUpdate": "manual",
  "sensorId": "test123",
  "metricEnums": 0,
  "metricValue": 55,
  "note": "repaired sensor",
  "dateTime": "2025-08-14T05:29:49.196Z"
}