using BuildingBlocks.CQRS;
using eShop_microservices.Catalog.API.Models;
using eShop_microservices.Catalog.API.Products.Dtos;
using Marten.Pagination;

namespace eShop_microservices.Catalog.API.Products.GetProduct {
    public sealed record GetProductsQuery(int pageSize=10,int pageNumber=1):IQuery<GetProductResponse>;
    public sealed record GetProductResponse(List<ProductDto> products);

    internal sealed class GetProductQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductResponse> {
       
        public async Task<GetProductResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken) {
          var products =  await session.Query<Product>().ToPagedListAsync(query.pageNumber, query.pageSize, cancellationToken);
            var dto = products.Select(p => new ProductDto() { Category = p.Category, Description = p.Description, Id = p.Id, ImageUrl = p.ImageUrl, Name = p.Name, Price = p.Price, Stock = p.Stock });
            return new GetProductResponse(dto.ToList());
        }
    }
}
