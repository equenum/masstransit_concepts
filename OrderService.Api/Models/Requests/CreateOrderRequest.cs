using System.Collections.Generic;
using Shared.Common.Models;

namespace OrderService.Api.Models.Requests;

public class CreateOrderRequest
{
    public int CustomerId { get; set; }    
    public DeliveryDetails DeliveryDetails { get; set; }
    public List<Product> Products { get; set; }
}
