
namespace eShop_microservices.Catalog.API.Products.UpdateProduct {
    public class UpdateProductEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapPut("/products", async (ISender sender, UpdateProductCommandRequest request) => {
                var response = await sender.Send(request);
                return Results.Ok(response);
            });
        }
    }
}
