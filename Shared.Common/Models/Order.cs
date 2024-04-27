using System.Collections.Generic;
using Shared.Common.Consts;

namespace Shared.Common.Models;

public class Order
{
public int Id { get; set; }
    public int CustomerId { get; set; }    
    public OrderType Type { get; set; }
    public DeliveryDetails DeliveryDetails { get; set; }
    public List<Product> Products { get; set; }
}
