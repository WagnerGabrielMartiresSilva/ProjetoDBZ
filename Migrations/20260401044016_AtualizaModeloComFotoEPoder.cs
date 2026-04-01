using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDBZ.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaModeloComFotoEPoder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "DBZ",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "PoderBase",
                table: "DBZ",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "DBZ");

            migrationBuilder.DropColumn(
                name: "PoderBase",
                table: "DBZ");
        }
    }
}
