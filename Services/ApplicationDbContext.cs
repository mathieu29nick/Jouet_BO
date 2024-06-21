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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jeton>()
            .HasKey(j => j.id);

            modelBuilder.Entity<Jeton>()
                .HasOne(j => j.Utilisateur)
                .WithOne(u => u.Jeton)
                .HasForeignKey<Jeton>(j => j.idUtilisateur);

            base.OnModelCreating(modelBuilder);
        }
    }
}
