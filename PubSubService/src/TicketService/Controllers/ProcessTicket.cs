using TicketService.Models;
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Dapr;

namespace TicketService.Controllers;

public class ProcessTicket
{
    [Topic("pubsub", "tickets")]
    public static async Task<IResult> Process(Ticket incomingTicket)
    {
        var ticket = incomingTicket ?? new Ticket("", "", 0, false);

        if (string.IsNullOrEmpty(ticket.Id))
        {
            Console.WriteLine("[INVENTORY] ❌ Received ticket with empty Id");
            return Results.BadRequest("Id is required");
        }
        if (ticket.Amount <= 0)
        {
            Console.WriteLine($"[INVENTORY] ❌ Received ticket {ticket.Id} with invalid amount: {ticket.Amount}");
            return Results.BadRequest("Amount must be positive");
        }

        Console.WriteLine($"[INVENTORY] 📦 Received ticket: {ticket.Id} (amount: ${ticket.Amount}) - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"[INVENTORY] ✅ Ticket {ticket.Id} processed successfully");

        return Results.Ok();
    }
}