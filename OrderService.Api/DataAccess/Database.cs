using System.Collections.Generic;

namespace OrderService.Api;

public static class Database
{
    public static readonly List<Order> Orders = new List<Order>()
    {
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
