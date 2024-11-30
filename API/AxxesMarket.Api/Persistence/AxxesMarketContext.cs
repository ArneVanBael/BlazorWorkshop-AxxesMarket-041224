using Microsoft.EntityFrameworkCore;
using AxxesMarket.Api.Domain;

namespace AxxesMarket.Api.Persistence;

public class AxxesMarketContext : DbContext
{
    public AxxesMarketContext(DbContextOptions<AxxesMarketContext> options)
            : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=AxxesMarket.db", options =>
        {
        });
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map table names
        modelBuilder.Entity<Product>().ToTable("Products", "dbo");
        
        modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Product>().Property(p => p.Id).IsRequired();
        modelBuilder.Entity<Product>().HasKey(p => p.Id);

        base.OnModelCreating(modelBuilder);
    }
}
