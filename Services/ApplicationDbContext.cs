using Back_Gestion.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_Gestion.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextoptions) : base(contextoptions) { 
            
        }

        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<Jeton> Jeton { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Produit> Produit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jeton>()
            .HasKey(j => j.id);

            modelBuilder.Entity<Jeton>()
                .HasOne(j => j.Utilisateur)
                .WithOne(u => u.Jeton)
                .HasForeignKey<Jeton>(j => j.idUtilisateur);

            modelBuilder.Entity<Categorie>()
                .HasKey(c => c.id);

            modelBuilder.Entity<Produit>()
                .HasKey(p => p.id);

            modelBuilder.Entity<Produit>()
                .HasOne(p => p.categorie)
                .WithMany(c => c.produit)
                .HasForeignKey(p => p.idCategorie)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
