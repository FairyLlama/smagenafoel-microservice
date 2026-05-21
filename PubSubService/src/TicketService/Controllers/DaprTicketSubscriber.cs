using TicketService.Models;


namespace TicketService.Controllers;

public class DaprTicketSubscriber
{
    public static object[] Subscribe()
    {
        return [
        new {
            pubsubname = "pubsub",
            topic = "tickets",
            route = "/tickets"
        }
    ];
    }
    public record PubSubModel
    (
        string pubsubname,
        string topic,
        string route
    );
}

