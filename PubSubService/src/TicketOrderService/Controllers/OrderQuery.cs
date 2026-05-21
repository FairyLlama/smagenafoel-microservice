using Dapr.Client;
using TicketOrderService.Models;
using Microsoft.AspNetCore.Http;

namespace TicketOrderService.Controllers;

public class OrderQuery
{
    public static async Task<IResult> FetchTicketOrder(string Id, DaprClient dapr)
    {
        var ticket = await dapr.GetStateAsync<Ticket>("statestore", Id);
        if (ticket == null)
        {
            return Results.NotFound();
        }
        return Results.Ok<Ticket>(ticket);
    }
}