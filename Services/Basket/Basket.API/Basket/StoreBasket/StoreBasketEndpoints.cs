using System.Reflection;
using Basket.API.Basket.GetBasket;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async(ISender sender, StoreBasketRequest request) =>
            {

                var command = request.Adapt<StoreBasketCommand>();


                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.UserName}", response);
            })
            .WithName("CreateProduct")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }

        //private async Task<IResult> HandleAsync(ISender sender, StoreBasketRequest request)
        //{
        //    var result = await sender.Send(new StoreBasketCommand(request.Cart));

        //    var response = result.Adapt<StoreBasketResponse>();

        //    return Results.Ok(response);
        //}
    }
}
