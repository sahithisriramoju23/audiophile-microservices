using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.API.Models;

namespace Product.API.Data.Configurations;

public class ProductGalleryImageConfiguration : IEntityTypeConfiguration<ProductGalleryImage>
{
    public void Configure(EntityTypeBuilder<ProductGalleryImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Position).IsRequired();
        builder.Property(x => x.CloudinaryPublicId).IsRequired();
        builder.Property(x => x.CloudinaryVersion).IsRequired();
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);

    }
}
