using CinemaApp.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieSubImg> MovieSubImgs { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryEntityTypeConfiguration).Assembly);

        modelBuilder.Entity<MovieActor>().HasKey(e => new { e.MovieId, e.ActorId });
    }
}
