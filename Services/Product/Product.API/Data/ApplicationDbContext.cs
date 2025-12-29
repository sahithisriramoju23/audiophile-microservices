using Microsoft.EntityFrameworkCore;
using Product.API.Models;

namespace Product.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<ProductEntity> Products { get; set; } = default!;
    public DbSet<ProductImage> ProductImages { get; set; } = default!;
    public DbSet<ProductGalleryImage> ProductGalleryImages { get; set; } = default!;
    public DbSet<ProductIncludedItem> ProductIncludedItems { get; set; } = default!;
}
