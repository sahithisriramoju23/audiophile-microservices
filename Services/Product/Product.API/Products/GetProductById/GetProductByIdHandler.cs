using BuidlingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;

namespace Product.API.Products.GetProductById;

public class GetProductByIdHandler(ApplicationDbContext _dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.Where(x=>x.Id == query.ProductId)
            .Include(x=>x.ProductImages)
            .Include(x => x.ProductGalleryImages)
            .Include(x => x.ProductIncludedItems).FirstOrDefaultAsync(cancellationToken);
        
        if (product == null) throw new Exception("Product is not found");
        var productDto = MapProductToDto(product);
        return new GetProductByIdResult(productDto);
    }
    public ProductDto MapProductToDto(ProductEntity product)
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
        return productDto;
    }
}
