using BuildingBlocks.CQRS;
using eShop_microservices.Catalog.API.Exceptions;
using eShop_microservices.Catalog.API.Models;
using eShop_microservices.Catalog.API.Products.Dtos;

namespace eShop_microservices.Catalog.API.Products.GetProductById {
    
    public sealed record GetProductByIdQueryRequest(Guid Id):IQuery<GetProductByIdQueryResponse>;
    public sealed record GetProductByIdQueryResponse(ProductDto productDto);
    
    internal sealed class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse> {
        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken) {

            Product? product = await session.LoadAsync<Product>(request.Id,cancellationToken);

            if (product is null) throw new ProductNotFoundException();

            return new GetProductByIdQueryResponse(product.MapProduct());
        }
    }
}
