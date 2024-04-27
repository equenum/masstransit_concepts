using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Common.DataAccess;
using Shared.Common.Messages;
using Shared.Common.Models;

namespace DeliveryService;

public class RegisterOrderConsumer : IConsumer<RegisterOrder>
{
    readonly ILogger<RegisterOrderConsumer> _logger;

    public RegisterOrderConsumer(ILogger<RegisterOrderConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<RegisterOrder> context)
    {
        _logger.LogInformation("Received message: {correlationId}", context.CorrelationId);

        Database.Orders.Add(new Order()
        {
            Id = Database.Orders.Max(order => order.Id) + 1,
            CustomerId = context.Message.CustomerId,
            Type = context.Message.Type,
            DeliveryDetails = context.Message.DeliveryDetails,
            Products = context.Message.Products
        });
        
        // todo send order registered message

        return Task.CompletedTask;
    }
}
