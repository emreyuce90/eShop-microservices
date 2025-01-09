
using eShop_microservices.Catalog.API.Products.CreateProduct;

namespace eShop_microservices.Catalog.API.Products.DeleteProduct {
    public class DeleteProductEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapDelete("/products/{id}", async (Guid Id, ISender sender) => {
                var response = await sender.Send(new DeleteProductCommandRequest(Id));
                return Results.Ok(response);

            }).WithName("DeleteProduct")
            .Produces<DeleteProductCommandResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product"); ;
        }
    }
}
