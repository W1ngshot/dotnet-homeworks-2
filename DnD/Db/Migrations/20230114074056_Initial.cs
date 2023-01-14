using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Db.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "monsters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    health = table.Column<int>(type: "integer", nullable: false),
                    attackmodifier = table.Column<int>(name: "attack_modifier", type: "integer", nullable: false),
                    attacksperround = table.Column<int>(name: "attacks_per_round", type: "integer", nullable: false),
                    rollscount = table.Column<int>(name: "rolls_count", type: "integer", nullable: false),
                    dicesize = table.Column<int>(name: "dice_size", type: "integer", nullable: false),
                    damagemodifier = table.Column<int>(name: "damage_modifier", type: "integer", nullable: false),
                    armor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_monsters", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "monsters",
                columns: new[] { "id", "armor", "attack_modifier", "attacks_per_round", "damage_modifier", "dice_size", "health", "name", "rolls_count" },
                values: new object[,]
                {
                    { 1, 13, 4, 1, 2, 8, 50, "Wolf", 2 },
                    { 2, 11, 2, 1, 2, 8, 50, "Sea elf", 2 },
                    { 3, 11, 2, 1, 1, 8, 35, "Hyena", 1 },
                    { 4, 15, 4, 1, 0, 8, 70, "Gnoll", 5 },
                    { 5, 17, 3, 1, 0, 6, 60, "Flying sword", 5 },
                    { 6, 15, 3, 1, 0, 8, 50, "Orond Gralhund", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "monsters");
        }
    }
}
