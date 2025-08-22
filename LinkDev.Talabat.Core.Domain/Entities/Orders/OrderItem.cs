using LinkDev.Talabat.Core.Domain.Entities.Orders;

public class OrderItem : BaseAuditableEntity<int>
{
    public OrderItem()
    {
        
    }
    public virtual ProductItemOrdered product { get; set; } // make virtual
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
