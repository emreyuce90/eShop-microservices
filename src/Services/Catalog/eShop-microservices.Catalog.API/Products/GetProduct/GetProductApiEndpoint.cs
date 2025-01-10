
using eShop_microservices.Catalog.API.Products.CreateProduct;
using eShop_microservices.Catalog.API.Products.Dtos;

namespace eShop_microservices.Catalog.API.Products.GetProduct {
    public class GetProductApiEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapGet("/products", async (ISender sender,[AsParameters] GetProductsQuery query) => {
                var response = await sender.Send(query);
                return Results.Ok(response);
            }).WithName("GetProduct")
            .Produces<List<ProductDto>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product")
            .WithDescription("Get Product"); ;
        }

  
    }
}
