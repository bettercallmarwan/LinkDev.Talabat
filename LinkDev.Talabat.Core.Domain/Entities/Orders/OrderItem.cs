namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    //table
    public class OrderItem : BaseAuditableEntity<int>
    {
        public required ProductItemOrdered product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
