using BuildingBlocks.CQRS;
using eShop_microservices.Catalog.API.Models;
using FluentValidation;

namespace eShop_microservices.Catalog.API.Products.CreateProduct {

    public sealed record CreateProductCommandRequest(string name, string description, decimal price, int stock, List<string> category, string imageUrl) : ICommand<CreateProductCommandResult>;

    public sealed record CreateProductCommandResult(Guid Id);

    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest> {
        public CreateProductValidator() {
            RuleFor(x => x.name).NotEmpty().WithMessage("Name area is required");
            RuleFor(x => x.description).NotEmpty().WithMessage("Description area is required");
            RuleFor(x => x.category).NotEmpty().WithMessage("Category area is required");
            RuleFor(x => x.imageUrl).NotEmpty().WithMessage("Image area is required");
            RuleFor(x => x.stock).NotNull().WithMessage("Stock area cannot be null");
            RuleFor(x => x.price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");
        }
    }

    internal sealed class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommandRequest, CreateProductCommandResult> {
        public async Task<CreateProductCommandResult> Handle(CreateProductCommandRequest command, CancellationToken cancellationToken) {
            //add create logic

            var p = new Product() {
                Category = command.category,
                Description = command.description,
                Stock = command.stock,
                ImageUrl = command.imageUrl,
                Name = command.name,
                Price = command.price,
            };
            session.Store(p);
            await session.SaveChangesAsync();
            return new CreateProductCommandResult(p.Id);
        }
    }
}
