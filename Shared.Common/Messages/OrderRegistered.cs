namespace Shared.Common.Messages;

public record OrderRegistered
{
    public int OrderId { get; init; }    
    public int CustomerId { get; init; }    
}
