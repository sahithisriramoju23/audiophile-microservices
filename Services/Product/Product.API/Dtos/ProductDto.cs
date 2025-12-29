using Product.API.Enums;

namespace Product.API.Models;

public record ProductDto
{
    public Guid Id { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsNew { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public int Quantity { get; set; }
    public string Features { get; set; } = default!;
   
    public string Category { get; set; } = default!;
   
    public List<ProductImageDto> Images { get; set; } = default!;
   
    public List<ProductGalleryImageDto> Gallery { get; set; } = default!;
   
    public List<ProductIncludedItemDto> IncludedItems { get; set; } = default!;
}
public record ProductImageDto
{
    public string ImageType { get; set; }
    public string CloudinaryPublicId { get;  set; } = default!;
    public string CloudinaryVersion { get;  set; } = default!;
}
public record ProductGalleryImageDto
{
    public int Position { get; set; }
    public string CloudinaryPublicId { get;  set; } = default!;
    public string CloudinaryVersion { get;  set; } = default!;

}
public class ProductIncludedItemDto
{
    public int Quantity { get; set; }
    public string Item { get; set; } = default!;
}
