
namespace eShop_microservices.Catalog.API.Products.DeleteProduct {
    public class DeleteProductEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapDelete("/products/{id}", async (Guid Id, ISender sender) => {
                var response = await sender.Send(new DeleteProductCommandRequest(Id));
                return Results.Ok(response);

            });
        }
    }
}
