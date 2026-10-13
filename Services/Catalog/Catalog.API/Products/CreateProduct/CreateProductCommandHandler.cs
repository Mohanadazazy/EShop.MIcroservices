

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand
    (
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    ) : ICommand<CreateProductResponse>;

    public record CreateProductResponse(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category Is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile Is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Should Be Greater Than Zero");

        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session) 
        : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {


            var product = new Product()
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResponse(product.Id);
        }
    }
}
