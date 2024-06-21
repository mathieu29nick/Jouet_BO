using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_Gestion.Migrations
{
    /// <inheritdoc />
    public partial class fisrtMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pseudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mdp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Jeton",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUtilisateur = table.Column<int>(type: "int", nullable: false),
                    nombreJeton = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jeton", x => x.id);
                    table.ForeignKey(
                        name: "FK_Jeton_Utilisateur_idUtilisateur",
                        column: x => x.idUtilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jeton_idUtilisateur",
                table: "Jeton",
                column: "idUtilisateur",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jeton");

            migrationBuilder.DropTable(
                name: "Utilisateur");
        }
    }
}
