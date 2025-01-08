using eShop_microservices.Catalog.API.Products.Dtos;

namespace eShop_microservices.Catalog.API.Models {
    public class Product {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public List<string> Category { get; set; } = new();


        public ProductDto MapProduct() => new ProductDto() {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            Stock = Stock,
            ImageUrl = ImageUrl,
            Category = Category,

        };
    }
}
