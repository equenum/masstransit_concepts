using System.Collections.Generic;
using Shared.Common.Consts;
using Shared.Common.Models;

namespace Shared.Common.Messages;

public record RegisterOrder
{
    public int CustomerId { get; set; }    
    public OrderType Type { get; set; }
    public DeliveryDetails DeliveryDetails { get; set; }
    public List<Product> Products { get; set; }
}
