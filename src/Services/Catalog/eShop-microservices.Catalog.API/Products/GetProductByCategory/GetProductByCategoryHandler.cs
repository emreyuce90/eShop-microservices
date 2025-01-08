using BuildingBlocks.CQRS;
using eShop_microservices.Catalog.API.Models;
using eShop_microservices.Catalog.API.Products.Dtos;
using Marten.Linq.QueryHandlers;

namespace eShop_microservices.Catalog.API.Products.GetProductByCategory {
    
    public sealed record GetProductByCategoryQueryRequest(string categoryname):IQuery<GetProductByCategoryQueryResponse>;
    public sealed record GetProductByCategoryQueryResponse(List<ProductDto> productDtos);



    internal sealed class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQueryRequest, GetProductByCategoryQueryResponse> {
        public async Task<GetProductByCategoryQueryResponse> Handle(GetProductByCategoryQueryRequest request, CancellationToken cancellationToken) {
            
            var productList = (List<Product>)await session.Query<Product>().Where(p => p.Category.Contains(request.categoryname)).ToListAsync(cancellationToken);
            
            return new GetProductByCategoryQueryResponse(productList.Select(p => new ProductDto() { Category = p.Category, Description = p.Description, Id = p.Id, ImageUrl = p.ImageUrl, Name = p.Name, Price = p.Price, Stock = p.Stock }).ToList());
        }
    }
}
