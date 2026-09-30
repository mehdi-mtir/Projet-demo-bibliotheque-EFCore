using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bibliotheque.Demo;

public class BibliothequeContext : DbContext
{
    public DbSet<Livre> Livres => Set<Livre>();
    public DbSet<Auteur> Auteurs => Set<Auteur>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=bibliotheque.db")
        .LogTo(Console.WriteLine, LogLevel.Information);
        //.EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Livre>()
            .Property(l => l.Titre)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Livre>()
            .HasOne(l => l.Auteur)
            .WithMany(a => a.Livres)
            .HasForeignKey(l => l.AuteurId);
    }
}
