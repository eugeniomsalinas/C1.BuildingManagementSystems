using C1.BuildingManagementSystems.Contracts.Models;
using C1.BuildingManagementSystems.DataAccess.Interfaces;
using C1.BuildingManagementSystems.Logging.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace C1.BuildingManagementSystems.DataAccess
{
    public class BmsDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public BmsDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("bmsApiDb"));
        }

        public DbSet<MetricEntry> MetricEntries { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
