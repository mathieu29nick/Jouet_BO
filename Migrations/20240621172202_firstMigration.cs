﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_Gestion.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.id);
                });

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
                name: "Produit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prixjeton = table.Column<int>(type: "int", nullable: false),
                    datepublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idCategorie = table.Column<int>(type: "int", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit", x => x.id);
                    table.ForeignKey(
                        name: "FK_Produit_Categorie_idCategorie",
                        column: x => x.idCategorie,
                        principalTable: "Categorie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Produit_idCategorie",
                table: "Produit",
                column: "idCategorie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jeton");

            migrationBuilder.DropTable(
                name: "Produit");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Categorie");
        }
    }
}
