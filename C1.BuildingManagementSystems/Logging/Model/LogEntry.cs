namespace C1.BuildingManagementSystems.Logging.Model
{
    public class LogEntry
    {
        public int LogEntryId { get; set; }
        public DateTime EntryDate { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string Type {  get; set; }
    }
}
