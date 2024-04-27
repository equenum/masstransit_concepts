using System.Collections.Generic;

namespace Shared.Common.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }    
    public DeliveryDetails DeliveryDetails { get; set; }
    public List<Product> Products { get; set; }
    public OrderStatus Status { get; set; }
}
