using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FailsSafeRentals.ConsoleApp.Migrations;

/// <inheritdoc />
public partial class _20261115232141_SeedExtraFeatures : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "ExtraFeatures",
            columns: new[] { "Id", "DailyPrice", "Name" },
            values: new object[,]
            {
                { 1, 75m, "GPS Navigation" },
                { 2, 120m, "Takbox Thule" },
                { 3, 50m, "Barnstol" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "ExtraFeatures",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "ExtraFeatures",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "ExtraFeatures",
            keyColumn: "Id",
            keyValue: 3);
    }
}
