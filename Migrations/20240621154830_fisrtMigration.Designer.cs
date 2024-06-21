﻿// <auto-generated />
using Back_Gestion.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Back_Gestion.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240621154830_fisrtMigration")]
    partial class fisrtMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Back_Gestion.Models.Jeton", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("idUtilisateur")
                        .HasColumnType("int");

                    b.Property<int>("nombreJeton")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("idUtilisateur")
                        .IsUnique();

                    b.ToTable("Jeton");
                });

            modelBuilder.Entity("Back_Gestion.Models.Utilisateur", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("mdp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pseudo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Utilisateur");
                });

            modelBuilder.Entity("Back_Gestion.Models.Jeton", b =>
                {
                    b.HasOne("Back_Gestion.Models.Utilisateur", "Utilisateur")
                        .WithOne("Jeton")
                        .HasForeignKey("Back_Gestion.Models.Jeton", "idUtilisateur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("Back_Gestion.Models.Utilisateur", b =>
                {
                    b.Navigation("Jeton")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
