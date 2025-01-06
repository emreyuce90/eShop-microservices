using BuildingBlocks.CQRS;
using eShop_microservices.Catalog.API.Models;
namespace eShop_microservices.Catalog.API.Products.CreateProduct {

    public sealed record CreateProductCommandQuery(string name,string description,decimal price,int stock,List<string> category,string imageUrl):ICommand<CreateProductCommandResult>;
   
    public sealed record CreateProductCommandResult(Guid Id);
    
    
    internal sealed class CreateProductHandler : ICommandHandler<CreateProductCommandQuery, CreateProductCommandResult> {
        public async Task<CreateProductCommandResult> Handle(CreateProductCommandQuery command, CancellationToken cancellationToken) {
            //add create logic

            var p = new Product() {
                Category = command.category,
                Description = command.description,
                Stock = command.stock,
                ImageUrl = command.imageUrl,
                Name = command.name,
                Price = command.price,
            };

            return new CreateProductCommandResult(Guid.NewGuid());
        }
    }
}
