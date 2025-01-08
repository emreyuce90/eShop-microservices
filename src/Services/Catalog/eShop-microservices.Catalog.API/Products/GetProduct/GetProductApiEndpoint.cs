
namespace eShop_microservices.Catalog.API.Products.GetProduct {
    public class GetProductApiEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapGet("/products", async (ISender sender) => {
                var response = await sender.Send(new GetProductsQuery());
                return Results.Ok(response);
            });
        }

  
    }
}
