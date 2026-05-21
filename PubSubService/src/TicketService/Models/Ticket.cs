namespace TicketService.Models;

public record Ticket(string Id, string EventName, int Amount, bool Available);