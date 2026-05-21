using System.Text;
using Dapr.Client;
using TicketOrderService.Controllers;
using TicketOrderService.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.MapPost("/tickets", async (Ticket ticket, DaprClient dapr) => await DaprTicketPublisher.Publish(ticket, dapr));
app.MapGet("/tickets/{id}", async (string Id, DaprClient dapr) => await OrderQuery.FetchTicketOrder(Id, dapr));
app.MapGet("/dapr/subscribe", async () => await TicketOrderSubscriber.Subscribe());

app.MapGet("/healthz", async (DaprClient dapr) =>
{
    try
    {
        var metadata = await dapr.GetMetadataAsync();
        return Results.Ok(new { status = "healthy", dapr = metadata });
    }
    catch
    {
        return Results.StatusCode(503);
    }
});

app.Run("http://0.0.0.0:8083");