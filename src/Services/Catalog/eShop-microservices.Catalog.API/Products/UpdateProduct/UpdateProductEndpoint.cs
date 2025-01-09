
using eShop_microservices.Catalog.API.Products.CreateProduct;

namespace eShop_microservices.Catalog.API.Products.UpdateProduct {
    public class UpdateProductEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapPut("/products", async (ISender sender, UpdateProductCommandRequest request) => {
                var response = await sender.Send(request);
                return Results.Ok(response);
            }).WithName("UpdateProduct")
            .Produces<UpdateProductCommandResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product"); ;
        }
    }
}
