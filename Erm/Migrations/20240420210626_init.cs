using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erm.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Domain = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessProcessId = table.Column<int>(type: "int", nullable: false),
                    RiskName = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    OccurrenceProbability = table.Column<int>(type: "INTEGER", nullable: false),
                    PotentialBusinessImpact = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskProfiles_BusinessProcess_BusinessProcessId",
                        column: x => x.BusinessProcessId,
                        principalTable: "BusinessProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskProfiles_BusinessProcessId",
                table: "RiskProfiles",
                column: "BusinessProcessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskProfiles");

            migrationBuilder.DropTable(
                name: "BusinessProcess");
        }
    }
}
