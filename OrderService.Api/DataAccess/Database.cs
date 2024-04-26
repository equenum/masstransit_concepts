﻿using System.Collections.Generic;
using OrderService.Api.Models;

namespace OrderService.Api;

public static class Database
{
    public static readonly List<Order> Orders = new List<Order>()
    {
        // todo properly populate the models
        new Order()
        {
            Id = 1
        },
        new Order() 
        { 
            Id = 2
        },
        new Order()
        {
            Id = 3
        }
    };
}
