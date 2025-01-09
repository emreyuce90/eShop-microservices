using BuildingBlocks.CQRS;
using eShop_microservices.Catalog.API.Models;
using eShop_microservices.Catalog.API.Products.Dtos;

namespace eShop_microservices.Catalog.API.Products.GetProduct {
    public sealed record GetProductsQuery():IQuery<GetProductResponse>;
    public sealed record GetProductResponse(List<ProductDto> products);

    internal sealed class GetProductQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductResponse> {
        public async Task<GetProductResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken) {
          var products =  await session.Query<Product>().ToListAsync(cancellationToken);
            var dto = products.Select(p => new ProductDto() { Category = p.Category, Description = p.Description, Id = p.Id, ImageUrl = p.ImageUrl, Name = p.Name, Price = p.Price, Stock = p.Stock });
            return new GetProductResponse(dto.ToList());
        }
    }
}
