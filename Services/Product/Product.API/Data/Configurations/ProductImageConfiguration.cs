using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.API.Enums;
using Product.API.Models;

namespace Product.API.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ImageType).IsRequired()
            .HasConversion(x => x.ToString(), dbValue => ProductEntity.ParseImageType(dbValue))
            .HasDefaultValue(ImageType.Thumbnail);

        builder.Property(x => x.CloudinaryPublicId).IsRequired();
        builder.Property(x => x.CloudinaryVersion).IsRequired();
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
        
    }
}
