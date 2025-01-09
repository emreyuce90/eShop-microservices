using BuildingBlocks.CQRS;
using eShop_microservices.Catalog.API.Exceptions;
using eShop_microservices.Catalog.API.Models;
using eShop_microservices.Catalog.API.Products.Dtos;
using FluentValidation;

namespace eShop_microservices.Catalog.API.Products.UpdateProduct {
    
    
    public sealed record UpdateProductCommandRequest(Guid Id,
     string Name,
     string Description ,
     decimal Price ,
     int Stock ,
     string? ImageUrl ,
     List<string> Category):ICommand<UpdateProductCommandResponse>;

    public sealed record UpdateProductCommandResponse(ProductDto productDto);

    public class UpdateProductValidator:AbstractValidator<UpdateProductCommandRequest> {
        public UpdateProductValidator() {
            RuleFor(x => x.Id).NotNull().WithMessage("Id area is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name area is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description area is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category area is required");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Image area is required");
            RuleFor(x => x.Stock).NotNull().WithMessage("Stock area cannot be null");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");
        }
    }

    internal sealed class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommandRequest, UpdateProductCommandResponse> {
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken) {
            //find data from db
            var product = await session.LoadAsync<Product>(request.Id);
            if (product is null) throw new ProductNotFoundException("Product",request.Id);
            //update db
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.ImageUrl = request.ImageUrl;
            product.Category = request.Category;

            await session.SaveChangesAsync();
            //return client
            return new UpdateProductCommandResponse(product.MapProduct());
        }
    }
}
