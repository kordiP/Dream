using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream.Data.Models;

public class DreamContext : DbContext
{
    public DreamContext()
    { }

    public DreamContext(DbContextOptions<DreamContext> options)
        : base(options)
    { }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Download> Downloads { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<GameDeveloper> GamesDevelopers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConfigurationData.connectionString)
            .UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.DeveloperId).HasName("PK_Developer");

            entity.HasIndex(e => e.Email, "UK_Email_Developer").IsUnique();
        });

        modelBuilder.Entity<Download>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.GameId });

            entity.HasOne(d => d.Game).WithMany(p => p.Downloads)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Downloads_Games");

            entity.HasOne(d => d.User).WithMany(p => p.Downloads)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Downloads_Users");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK_Game");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK_Genre");

            modelBuilder.Entity<Genre>()
            .HasMany(c => c.Games)
            .WithOne(e => e.Genre)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.GameId });

            entity.HasOne(d => d.Game).WithMany(p => p.Likes)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Likes_Games");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Likes_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.HasIndex(e => e.Email, "UK_Email_User").IsUnique();

            entity.HasIndex(e => e.Username, "UK_Username_User").IsUnique();
        });

        modelBuilder.Entity<GameDeveloper>(entity =>
        {
            entity.HasKey(e => new { e.GameId, e.DeveloperId });

            entity.HasOne(d => d.Game).WithMany(p => p.GameDevelopers)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_GamesDevelopers_Games");

            entity.HasOne(d => d.Developer).WithMany(p => p.GameDevelopers)
                .HasForeignKey(d => d.DeveloperId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_GamesDevelopers_Developers");
        });

        base.OnModelCreating(modelBuilder);
    }
}
