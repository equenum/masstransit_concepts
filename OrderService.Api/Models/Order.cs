﻿using System.Collections.Generic;
using OrderService.Api.Consts;

namespace OrderService.Api.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }    
    public OrderType Type { get; set; }
    public DeliveryDetails DeliveryDetails { get; set; }
    public List<Product> Products { get; set; }
}
