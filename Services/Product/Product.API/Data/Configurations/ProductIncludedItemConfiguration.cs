using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.API.Models;

namespace Product.API.Data.Configurations;

public class ProductIncludedItemConfiguration : IEntityTypeConfiguration<ProductIncludedItem>
{
    public void Configure(EntityTypeBuilder<ProductIncludedItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
    }
}
