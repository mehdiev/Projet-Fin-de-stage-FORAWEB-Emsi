using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foramag.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_Login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "V_Clt",
                columns: table => new
                {
                    Num_Client = table.Column<int>(type: "int", nullable: false),
                    Intitule_Client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ancien_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categorie_Tarifaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solvabilite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commercial = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_Login");

            migrationBuilder.DropTable(
                name: "V_Clt");
        }
    }
}
