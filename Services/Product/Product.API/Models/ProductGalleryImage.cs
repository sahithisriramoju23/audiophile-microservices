namespace Product.API.Models;

public class ProductGalleryImage
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Position { get; set; }
    public string CloudinaryPublicId { get; private set; } = default!;
    public string CloudinaryVersion { get; private set; } = default!;
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    //for ef core
    private ProductGalleryImage() { }
    public ProductGalleryImage(Guid productId, int position, string cloudinaryPublicId, string cloudinaryVersion)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Position = position;
        CloudinaryPublicId = cloudinaryPublicId;
        CloudinaryVersion = cloudinaryVersion;
        CreatedAt = DateTime.UtcNow;
    }
}