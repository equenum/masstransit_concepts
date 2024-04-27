using System;
using MassTransit;

namespace DeliveryService.Consumers;

public class OrderRegisteredConsumerDefinition : ConsumerDefinition<OrderRegisteredConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<OrderRegisteredConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.PrefetchCount = 10;
        endpointConfigurator.UseMessageRetry(retry => 
        {
            retry.Immediate(3);
        });

        consumerConfigurator.Options<BatchOptions>(options => 
        {
            options.SetMessageLimit(3);
            options.SetConcurrencyLimit(1);
            options.SetTimeLimit(TimeSpan.FromSeconds(10));
        });
    }
}
