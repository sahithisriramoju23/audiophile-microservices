using Product.API.Models;

namespace Product.API.Data.Extensions;

public static class InitialData
{
    //convert this json to objects of type ProductEntity
    
    public static IEnumerable<ProductEntity> GetProducts() => new List<ProductEntity>
        {
        new ProductEntity(
            slug: "yx1-earphones",name: "YX1 Wireless Earphones", isnew: true, category: "earphones",price: 599,
            features: "Experience unrivalled stereo sound thanks to innovative acoustic technology. With improved ergonomics designed for full day wearing, these revolutionary earphones have been finely crafted to provide you with the perfect fit, delivering complete comfort all day long while enjoying exceptional noise isolation and truly immersive sound.\n\nThe YX1 Wireless Earphones features customizable controls for volume, music, calls, and voice assistants built into both earbuds. The new 7-hour battery life can be extended up to 28 hours with the charging case, giving you uninterrupted play time. Exquisite craftsmanship with a splash resistant design now available in an all new white and grey color scheme as well as the popular classic black.",
            description: "Tailor your listening experience with bespoke dynamic drivers from the new YX1 Wireless Earphones. Enjoy incredible high-fidelity sound even in noisy environments with its active noise cancellation feature.",
            quantity: 50),
        new ProductEntity(
            slug: "xx59-headphones",name: "XX59 Headphones", isnew: true, category: "headphones",price: 2999,
            features: "These headphones have been created from durable, high-quality materials tough enough to withstand daily use. Its compact folding design means you can easily store them in your bag. The sleek metal headband features an adjustable slider with reinforced steel sliders to provide a perfect fit every time.\n\nThe XX59 headphones delivers excellent sound quality whether you’re in the studio, on the stage or on the move. From punchy bass to crystal clear highs, the 40mm drivers deliver an all-round balanced sound. Its closed-back design reduces sound leakage, giving you an immersive listening experience with a rich, detailed sound.",
            description: "Enjoy your audio almost anywhere and customize it to your specific tastes with the XX59 headphones. The stylish yet durable versatile wireless headset is a brilliant companion at home or on the move.",
            quantity: 100),
        new ProductEntity(
            slug: "xx99-mark-one-headphones",name: "XX99 Mark I Headphones", isnew: false, category: "headphones",price: 1750,
            features: "As the gold standard for headphones, the classic XX99 Mark I offers detailed and accurate audio reproduction for audiophiles, mixing engineers, and music aficionados alike in studios and on the go.\n\nFrom the stylish design to the robust build quality, the XX99 Mark I demonstrates Audiophile’s commitment to creating the perfect headphone. It includes a genuine leather head strap and premium earcups, which offer superior comfort for those who like to enjoy endless listening. Its closed-back design delivers excellent isolation, reducing background noise and delivering pure sound.",
            description: "The XX99 Mark I headphones is your perfect companion for immersive on-the-go listening. It features a genuine leather head strap and premium earcups designed to deliver comfort and durability.",
            quantity: 175),
        new ProductEntity(
            slug: "xx99-mark-two-headphones",name: "XX99 Mark II Headphones", isnew: true, category: "headphones",price: 2999,
            features: "The new XX99 Mark II headphones is the pinnacle of pristine audio. It redefines your premium headphone experience by reproducing the balanced depth and precision of studio-quality sound.\n\nFrom the elegant rose gold accents to the intuitive controls, the Mark II headphones encompasses Audiophile’s signature style and high-fidelity audio. It includes a genuine leather head strap and premium earcups, which offer superior comfort for those who like to enjoy endless listening. Its closed-back design delivers excellent isolation, reducing background noise and delivering pure sound.",
            description: "Featuring a genuine leather head strap and premium earcups, these headphones deliver superior comfort for those who like to enjoy endless listening. It features intuitive controls designed for any situation.",
            quantity: 250),
        new ProductEntity(
            slug: "zx9-speaker",name: "ZX9 Speaker", isnew: true, category: "speakers",price: 4500,
            features: "Upgrade your sound system with the all new ZX9 active speaker. It’s a bookshelf speaker system that offers truly wireless connectivity -- creating new possibilities for more pleasing and practical audio setups.",
            description: "Connect via Bluetooth or nearly any wired source. This speaker features optical, digital coaxial, USB Type-B, stereo RCA, and a headphone jack for maximum connectivity.",
            quantity: 75),
        new ProductEntity(
            slug: "zx7-speaker",name: "ZX7 Speaker", isnew: false, category: "speakers",price: 3500,
            features: "Stream high quality sound wirelessly with minimal to no loss. The ZX7 speaker uses high-end audiophile components that represents the top of the line powered speakers for home or studio use.",
            description: "Reap the benefits of a flat diaphragm tweeter cone. This provides a fast response time and excellent high frequency dispersion making the ZX7 ideal for any studio setting.",
            quantity: 60)
            };

    public static ProductImage GetProductThumbnailImage(Guid ProductId, string slug) => new ProductImage(
        productId: ProductId,
        imageType: Enums.ImageType.Thumbnail,
        cloudinaryPublicId: slug,
        cloudinaryVersion: "v1746186315"
        );

    public static ProductGalleryImage GetProductGalleryImage(Guid ProductId, string slug, int index) => new ProductGalleryImage(
        productId: ProductId,
        position: index,
        cloudinaryPublicId: $"{slug}-gallery-{index}",
        cloudinaryVersion: "v1746186315"
        );

    public static ProductIncludedItem GetProductIncludedItem(Guid ProductId, string itemName, int quantity) => new ProductIncludedItem(
        productId: ProductId,
        itemName: itemName,
        quantity: quantity
        );

    public static IEnumerable<ProductIncludedItem> GetEarphonesIncludedItems(Guid productId) => new List<ProductIncludedItem>
    {
        new ProductIncludedItem(productId,"Earphone unit",2),
        new ProductIncludedItem(productId,"Multi-size earplugs",2),
        new ProductIncludedItem(productId,"User manual",1),
        new ProductIncludedItem(productId,"USB-C charging cable",1),
        new ProductIncludedItem(productId,"Travel pouch",1),
    };
    public static IEnumerable<ProductIncludedItem> GetEarphonesProductIncludedItems(Guid productId) => new List<ProductIncludedItem>
    {
        new ProductIncludedItem(productId,"Headphone unit",1),
        new ProductIncludedItem(productId,"Replacement earcups",2),
        new ProductIncludedItem(productId,"User manual",1),
        new ProductIncludedItem(productId,"3.5mm 5m audio cable",1),
    };
    public static IEnumerable<ProductIncludedItem> GetSpeakerIncludedItems(Guid productId) => new List<ProductIncludedItem>
    {
        new ProductIncludedItem(productId,"Speaker unit",2),
        new ProductIncludedItem(productId,"Speaker cloth panel",2),
        new ProductIncludedItem(productId,"User manual",1),
        new ProductIncludedItem(productId,"3.5mm 7.5m audio cable",1),
        new ProductIncludedItem(productId,"7.5m optical cable",1),
    };

    public static IEnumerable<ProductIncludedItem> MapProductIncludedItems(List<ProductEntity> products)
    {
        List<ProductIncludedItem> productIncludedItems = new List<ProductIncludedItem>();

        foreach(var product in products)
        {
            if(product.Category == "earphones")
            {
                var includedItems = GetEarphonesIncludedItems(product.Id);
                productIncludedItems.AddRange(includedItems);
            }
            else if(product.Category == "headphones")
            {
                var includedItems = GetEarphonesProductIncludedItems(product.Id);
                productIncludedItems.AddRange(includedItems);
            }
            else if(product.Category == "speakers")
            {
                var includedItems = GetSpeakerIncludedItems(product.Id);
                productIncludedItems.AddRange(includedItems);
            }
        }
        return productIncludedItems;
    }
    
}
