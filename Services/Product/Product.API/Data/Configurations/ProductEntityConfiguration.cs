using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.API.Models;

namespace Product.API.Data.Configurations;

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Slug).IsRequired();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Description);
        builder.Property(p => p.IsNew).HasDefaultValue(false);
        builder.Property(p => p.Quantity).HasDefaultValue(0);
        builder.Property(p => p.Features);
        builder.Property(p => p.Category).IsRequired();
        builder.Property(p => p.CreatedAt);
        builder.Property(p => p.UpdatedAt);
        builder.HasIndex(x => x.Slug).IsUnique();
       
        builder.HasMany(p => p.ProductImages)
            .WithOne()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProductGalleryImages) 
            .WithOne()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProductIncludedItems)
            .WithOne()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
