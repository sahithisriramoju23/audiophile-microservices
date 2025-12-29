using Product.API.Enums;

namespace Product.API.Models;

public class ProductImage
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public ImageType ImageType { get; private set; }
    public string CloudinaryPublicId { get; private set; } = default!;
    public string CloudinaryVersion { get; private set; } = default!;
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    //for ef core
    private ProductImage() { }
    public ProductImage(Guid productId, ImageType imageType, string cloudinaryPublicId, string cloudinaryVersion)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ImageType = imageType;
        CloudinaryPublicId = cloudinaryPublicId;
        CloudinaryVersion = cloudinaryVersion;
        CreatedAt = DateTime.UtcNow;
    }
}
