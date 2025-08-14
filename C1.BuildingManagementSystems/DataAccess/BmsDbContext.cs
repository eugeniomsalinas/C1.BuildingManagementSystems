using C1.BuildingManagementSystems.Contracts.Models;
using C1.BuildingManagementSystems.Logging.Model;
using Microsoft.EntityFrameworkCore;

namespace C1.BuildingManagementSystems.DataAccess
{
    public class BmsDbContext : DbContext
    {
        public BmsDbContext(DbContextOptions<BmsDbContext> options)
            : base(options)
        {
        }

        public DbSet<MetricEntry> MetricEntries { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
