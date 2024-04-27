using System.Collections.Generic;
using Shared.Common.Models;

namespace Shared.Common.Messages;

public record RegisterOrder
{
    public int CustomerId { get; init; }    
    public DeliveryDetails DeliveryDetails { get; init; }
    public List<Product> Products { get; init; }
}
