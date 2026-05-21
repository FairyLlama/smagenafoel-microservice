using Dapr;
using Dapr.Client;
using TicketService.Controllers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using TicketService.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();
app.UseCloudEvents();

app.MapPost("/tickets", async (Ticket incomingTicket) => await ProcessTicket.Process(incomingTicket));

// Add subscription endpoint for Dapr discovery
app.MapGet("/dapr/subscribe", DaprTicketSubscriber.Subscribe);

// Health check endpoint
app.MapGet("/healthz", () =>
{
    return Results.Ok(new { status = "healthy", service = "inventory" });
});

app.Run("http://0.0.0.0:8082");