using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProducyById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:Guid}", async (ISender sender, Guid id) =>
            {
                var product = await sender.Send(new GetProductByIdQuery(id));
                return Results.Ok(product);
            })
            .WithName("GetProductById")
            .Produces<GetProductsResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
}
