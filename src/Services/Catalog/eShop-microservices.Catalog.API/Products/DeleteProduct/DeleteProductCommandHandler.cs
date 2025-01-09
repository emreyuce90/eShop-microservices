using BuildingBlocks.CQRS;
using eShop_microservices.Catalog.API.Exceptions;
using eShop_microservices.Catalog.API.Models;
using FluentValidation;

namespace eShop_microservices.Catalog.API.Products.DeleteProduct {
    
    public sealed record DeleteProductCommandRequest(Guid Id):ICommand<DeleteProductCommandResponse>;
    public sealed record DeleteProductCommandResponse(Guid Id);

    public class DeleteProductValidator :AbstractValidator<DeleteProductCommandRequest>{
        public DeleteProductValidator() {
            RuleFor(x => x.Id).NotEmpty().WithMessage("The Id area cannot be null");
        }
    }

    internal sealed class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommandRequest, DeleteProductCommandResponse> {
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken) {
            //find record from db
            var product = await session.LoadAsync<Product>(request.Id);
            //if not exist throw exception
            if (product is null) throw new ProductNotFoundException("Product",request.Id); 
            //delete record
            session.Delete(product);
            await session.SaveChangesAsync();
            //return client records id
            return new DeleteProductCommandResponse(request.Id);
        }
    }
}
