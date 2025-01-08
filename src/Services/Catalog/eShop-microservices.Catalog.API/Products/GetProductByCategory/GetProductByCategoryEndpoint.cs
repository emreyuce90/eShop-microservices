
using eShop_microservices.Catalog.API.Products.GetProductById;

namespace eShop_microservices.Catalog.API.Products.GetProductByCategory {
    public class GetProductByCategoryEndpoint : ICarterModule {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapGet("/products/category/{categoryName}", async (string categoryName,ISender sender) => {
                var response = await sender.Send(new GetProductByCategoryQueryRequest(categoryName));
                return Results.Ok(response);
            }).WithName("GetProductByCategoryName")
                .WithDescription("Get Product By Categoryname")
                .Produces<GetProductByCategoryQueryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest); ;
        }
    }
}
