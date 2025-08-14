using C1.BuildingManagementSystems.DataAccess;
using C1.BuildingManagementSystems.DataAccess.Interfaces;
using C1.BuildingManagementSystems.Logic;
using C1.BuildingManagementSystems.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BmsDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("bmsApiDb")));

builder.Services.AddScoped<IBuildingMetricsServiceLogic, BuildingMetricsServiceLogic>();
builder.Services.AddScoped<IMetricEntriesRepository, MetricEntriesRepository>();
builder.Services.AddScoped<ILoggingRepository, LoggingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
