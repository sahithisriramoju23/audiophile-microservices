using BuidlingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Products.GetProductByCategory;

public class GetProductsByCategoryHandler(ApplicationDbContext dbContext) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .Where(x => x.Category == query.Category)
            .Include(x => x.ProductImages)
            .Include(x => x.ProductGalleryImages)
            .Include(x => x.ProductIncludedItems)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        // Map ProductEntity to ProductDto as needed
        var productsDto = MapProductsToDtos(products);
        
        return new GetProductsByCategoryResult(productsDto);
    }
    public List<ProductDto> MapProductsToDtos(List<ProductEntity> products)
    {
        var productDtos = new List<ProductDto>();
        foreach (var product in products)
        {
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Slug = product.Slug,
                Images = product.ProductImages.Select(pi => new ProductImageDto
                {
                    CloudinaryPublicId = pi.CloudinaryPublicId,
                    CloudinaryVersion = pi.CloudinaryVersion,
                    ImageType = pi.ImageType.ToString(),
                }).ToList(),
                Gallery = product.ProductGalleryImages.Select(pgi => new ProductGalleryImageDto
                {
                    CloudinaryPublicId = pgi.CloudinaryPublicId,
                    CloudinaryVersion = pgi.CloudinaryVersion,
                    Position = pgi.Position
                }).ToList(),
                IncludedItems = product.ProductIncludedItems.Select(pii => new ProductIncludedItemDto
                {
                    Item = pii.Name,
                    Quantity = pii.Quantity
                }).ToList()
            };
            productDtos.Add(productDto);
        }
        return productDtos;
    }
}
