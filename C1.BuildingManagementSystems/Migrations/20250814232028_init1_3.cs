using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace C1.BuildingManagementSystems.Migrations
{
    /// <inheritdoc />
    public partial class init1_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MetricValue",
                table: "MetricEntries",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MetricValue",
                table: "MetricEntries",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
