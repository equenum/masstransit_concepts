using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Common.DataAccess;
using Shared.Common.Messages;
using Shared.Common.Models;

namespace DeliveryService.Consumers;

public class OrderRegisteredConsumer : IConsumer<Batch<OrderRegistered>>
{
    private readonly ILogger<OrderRegisteredConsumer> _logger;

    public OrderRegisteredConsumer(ILogger<OrderRegisteredConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<Batch<OrderRegistered>> context)
    {
        var messages = context.Message.Select(x => x.Message);
        _logger.LogInformation("Received order batch for delivery: {orderIds}.", string.Join(", ", messages.Select(x => x.OrderId)));

        foreach (var message in messages)
        {
            var dbOrder = Database.Orders.FirstOrDefault(o => o.Id == message.OrderId);
            if (dbOrder == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            await Task.Delay(1000);
            dbOrder.Status = OrderStatus.Completed;

            _logger.LogInformation("Order {orderId} delivered.", dbOrder.Id);
        }
    }
}
