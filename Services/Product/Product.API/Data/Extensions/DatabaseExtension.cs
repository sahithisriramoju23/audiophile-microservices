using Microsoft.EntityFrameworkCore;
using Product.API.Models;

namespace Product.API.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
        // Additional seeding logic can be added here if necessary
        await SeedProductsAsync(dbContext);
        await SeedProductImagesAsync(dbContext);
        await SeedProductIncludedItemsAsync(dbContext);
    }
    public static async Task SeedProductsAsync(DbContext dbContext)
    {
        if (!await dbContext.Set<ProductEntity>().AnyAsync())
        {
            var products = InitialData.GetProducts();
            await dbContext.Set<ProductEntity>().AddRangeAsync(products);
            await dbContext.SaveChangesAsync();
        }
    }
    public static async Task SeedProductImagesAsync(DbContext dbContext)
    {
        var products = await dbContext.Set<ProductEntity>().ToListAsync();
        foreach (var product in products)
        {
            if (!await dbContext.Set<ProductImage>().AnyAsync(pi => pi.ProductId == product.Id))
            {
                var thumbnail = InitialData.GetProductThumbnailImage(product.Id, product.Slug);
                await dbContext.Set<ProductImage>().AddAsync(thumbnail);
            }
            for (int i = 1; i <= 3; i++)
            {
                if (!await dbContext.Set<ProductGalleryImage>().AnyAsync(pgi => pgi.ProductId == product.Id && pgi.Position == i))
                {
                    var galleryImage = InitialData.GetProductGalleryImage(product.Id, product.Slug, i);
                    await dbContext.Set<ProductGalleryImage>().AddAsync(galleryImage);
                }
            }
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task SeedProductIncludedItemsAsync(DbContext dbContext)
    {
        var products = await dbContext.Set<ProductEntity>().ToListAsync();

        if (!await dbContext.Set<ProductIncludedItem>().AnyAsync())
        {
            IEnumerable<ProductIncludedItem> includedItems = InitialData.MapProductIncludedItems(products);
            await dbContext.Set<ProductIncludedItem>().AddRangeAsync(includedItems);
            await dbContext.SaveChangesAsync();
        }
    }
}