using Dapr.Client;
var client = new DaprClientBuilder().Build();

var order = new Order("order-123", 100);

await client.SaveStateAsync(
    "statestore",
    order.Id,
    order
);
var order_received = await client.GetStateAsync<Order>(
    "statestore",
    "order-123"
);

public record Order(string Id, int Amount);