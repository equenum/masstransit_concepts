using System.Collections.Generic;
using OrderService.Api.Consts;

namespace OrderService.Api.Models.Requests;

public class CreateOrderRequest
{
    public int CustomerId { get; set; }    
    public OrderType Type { get; set; }
    public DeliveryDetails DeliveryDetails { get; set; }
    public List<Product> Products { get; set; }
}
