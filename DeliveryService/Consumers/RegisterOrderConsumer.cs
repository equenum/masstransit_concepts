using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Common.DataAccess;
using Shared.Common.Messages;
using Shared.Common.Models;

namespace DeliveryService.Consumers;

public class RegisterOrderConsumer : IConsumer<RegisterOrder>
{
    readonly ILogger<RegisterOrderConsumer> _logger;

    public RegisterOrderConsumer(ILogger<RegisterOrderConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RegisterOrder> context)
    {
        _logger.LogInformation("Received an order registration message.");

        var  order = new Order()
        {
            Id = Database.Orders.Any() ? Database.Orders.Max(order => order.Id) + 1 : 1,
            CustomerId = context.Message.CustomerId,
            DeliveryDetails = context.Message.DeliveryDetails,
            Products = context.Message.Products,
            Status = OrderStatus.Registered
        };

        Database.Orders.Add(order);
        _logger.LogInformation("Order {orderId} created.", order.Id);
        await context.RespondAsync(new OrderRegistrationResult()
        {
            OrderId = order.Id
        });
        
        await context.Publish(new OrderRegistered()
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId
        });

        _logger.LogInformation("Order {orderId} registered.", order.Id);
    }
}
