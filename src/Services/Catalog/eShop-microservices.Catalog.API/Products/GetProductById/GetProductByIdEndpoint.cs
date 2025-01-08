
namespace eShop_microservices.Catalog.API.Products.GetProductById {
    public class GetProductByIdEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapGet("/products/{id}", async (Guid Id, ISender sender) => {
                var response = await sender.Send(new GetProductByIdQueryRequest(Id));
                return Results.Ok(response);
            }).WithName("GetProductById")
                .WithDescription("Get Product By Id")
                .Produces<GetProductByIdQueryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);

        }
    }
}
