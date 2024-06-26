﻿using System;
using System.IO;
using System.Reflection;
using DeliveryService.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Services.AddMassTransit(configurator => 
{
    configurator.SetKebabCaseEndpointNameFormatter();

    configurator.AddConsumer<RegisterOrderConsumer>();
    configurator.AddConsumer<OrderRegisteredConsumer, OrderRegisteredConsumerDefinition>();

    // adds all IConsumer<T> implementations, if all those are located in the same assembly
    // configurator.AddConsumers(typeof(Program).Assembly);

    // useful for modular monoliths or just testing 
    // configurator.UsingInMemory((context, configuration) => 
    // {
    //     configuration.UseMessageRetry(retry => 
    //     {
    //         retry.Immediate(3);
    //     });

    //     configuration.ConfigureEndpoints(context);
    // });

    configurator.UsingRabbitMq((context, configuration) => 
    {
        configuration.Host("localhost", "/", host => 
        {
            host.Username("guest");
            host.Password("guest");
        });

        configuration.UseMessageRetry(retry => 
        {
            retry.Immediate(3);
        });

        configuration.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();