using Catalog.API.Products.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.DeleteProduct
{

    

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}", async ([FromRoute] Guid id,ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                return Results.Ok(result);
            })
            .WithName("DeleteProduct")
            .Produces<UpdateProductResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
