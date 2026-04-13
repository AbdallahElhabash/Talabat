using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order() { }
        public Order(string buyerEmail, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotoal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotoal = subTotoal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();
        public decimal SubTotoal { get; set; }
        public string PaymentIntentId { get; set; }=string.Empty;
        public decimal GetTotal() => SubTotoal + DeliveryMethod.Cost;
        
    }
}
