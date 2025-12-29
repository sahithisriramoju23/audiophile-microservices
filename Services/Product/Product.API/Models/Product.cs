using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Product.API.Enums;

namespace Product.API.Models;

public class ProductEntity
{
    private List<ProductImage> _productImages = new List<ProductImage>();
    private List<ProductGalleryImage> _productGalleryImages = new List<ProductGalleryImage>();
    private List<ProductIncludedItem> _productIncludedItems = new List<ProductIncludedItem>();
    public Guid Id { get; private set; }
    public string Slug { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public bool IsNew { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; } = default!;
    public int Quantity { get; private set; }
    public string Features { get; private set; } = default!;
    public string Category { get; private set; } = default!;
    public IReadOnlyList<ProductImage> ProductImages => _productImages.AsReadOnly();
    public IReadOnlyList<ProductGalleryImage> ProductGalleryImages => _productGalleryImages.AsReadOnly();
    public IReadOnlyList<ProductIncludedItem> ProductIncludedItems => _productIncludedItems.AsReadOnly();
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    //for ef core
    private ProductEntity() { }
    public ProductEntity(string slug, string name, bool isnew, decimal price, string description, int quantity, string features, string category)
    {
        Id = Guid.NewGuid();
        Slug = slug;
        Name = name;
        IsNew = isnew;
        Price = price;
        Description = description;
        Quantity = quantity;
        Features = features;
        Category = category;
        CreatedAt = DateTime.UtcNow;
    }
    public void AddProductImage(string imageType, string cloudinaryPublicId, string cloudinaryVersion)
    {
        var parsedImageType = ParseImageType(imageType);
        _productImages.Add(new ProductImage(this.Id, parsedImageType, cloudinaryPublicId, cloudinaryVersion));
    }

    public void AddProductGalleryImage(string cloudinaryPublicId, string cloudinaryVersion, int position)
    {
        _productGalleryImages.Add(new ProductGalleryImage(this.Id, position, cloudinaryPublicId, cloudinaryVersion));
    }
    public void AddProductIncludedItem(string itemName, int quantity)
    {
        _productIncludedItems.Add(new ProductIncludedItem(this.Id, itemName, quantity));
    }
    public void RemoveProductGalleryImage(int position)
    {
        var image = _productGalleryImages.FirstOrDefault(img => img.Position == position);
        if (image != null)
        {
            _productGalleryImages.Remove(image);
        }
    }

    public static ImageType ParseImageType(string imageType) =>
     string.Equals(imageType, "thumbnail", StringComparison.OrdinalIgnoreCase) ? ImageType.Thumbnail :
            string.Equals(imageType, "cart", StringComparison.OrdinalIgnoreCase) ? ImageType.Cart :
            string.Equals(imageType, "category", StringComparison.OrdinalIgnoreCase) ? ImageType.Category :
            throw new ArgumentException("Invalid image type", nameof(imageType));

}





