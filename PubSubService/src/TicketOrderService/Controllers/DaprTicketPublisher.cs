using System.Text;
using Dapr.Client;
using TicketOrderService.Models;
using Microsoft.AspNetCore.Http;

namespace TicketOrderService.Controllers;

public class DaprTicketPublisher
{
    public static async Task<IResult> Publish(Ticket ticket, DaprClient dapr)
    {
        if (string.IsNullOrEmpty(ticket.Id))
        {
            return Results.BadRequest("TicketId is required");
        }
        if (ticket.Amount <= 0)
        {
            return Results.BadRequest("Amount must be positive");
        }

        try
        {
            await dapr.SaveStateAsync("statestore", ticket.Id, ticket);
            await dapr.PublishEventAsync("pubsub", "tickets", ticket);

            var metadata = new Dictionary<string, string>
            {
                ["blobName"] = $"{ticket.Id}.txt",
                ["key"] = $"{ticket.Id}.txt",
                ["fileName"] = $"{ticket.Id}.txt"
            };

            await dapr.InvokeBindingAsync(
                "storage",
                "create",
                Encoding.UTF8.GetBytes($"Ticket receipt for {ticket.Id}"),
                metadata
            );

            Console.WriteLine($"Ticket {ticket.Id} created successfully");
            return Results.Accepted();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing order {ticket.Id}: {ex.Message}");
            return Results.Problem("Failed to process order");
        }
    }
}