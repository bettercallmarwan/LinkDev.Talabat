﻿using LinkDev.Talabat.Core.Domain.Entities.Orders;


namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{

    public class Order : BaseAuditableEntity<int>
    {
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; }
        public virtual Address ShippingAddress { get; set; }

        public int? DeliveryMethodId { get; set; }
        public virtual DeliveryMethod? DeliveryMethod { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod!.Cost;
        public string PayementIntentId { get; set; } = "";
    }
}