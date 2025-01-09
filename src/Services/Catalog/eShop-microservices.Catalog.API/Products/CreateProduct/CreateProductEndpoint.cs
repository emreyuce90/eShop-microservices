namespace eShop_microservices.Catalog.API.Products.CreateProduct {

    public class CreateProductEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapPost("/products", async (CreateProductCommandRequest request,ISender sender) => {
                var res =  await sender.Send(request);
                return Results.Created($"/products/{res.Id}",res);

            })
            .WithName("CreateProduct")
            .Produces<CreateProductCommandResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}
