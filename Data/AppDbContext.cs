using csapi.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Entry> Entries { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<User> Users {get; set;}

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testdb;Username=pad;Password=password", o => o.UseNodaTime());
  }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Entry>()
      .HasOne(e => e.Category)
      .WithMany(c => c.Entries)
      .HasForeignKey(e => e.CategoryId);
  }
}
