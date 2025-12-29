namespace Product.API.Models;

public class ProductIncludedItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public string Name { get; private set; } = default!;
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    //for ef core
    private ProductIncludedItem() { }
    public ProductIncludedItem(Guid productId, string itemName, int quantity)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        ProductId = productId;
        Name = itemName;
        Quantity = quantity;
    }
}
