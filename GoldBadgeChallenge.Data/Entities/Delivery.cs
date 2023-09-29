using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldBadgeChallenge.Data.Entities.Enums;

namespace GoldBadgeChallenge.Data.Entities
{
    public class Delivery
    {
        public Delivery()
        {
            
        }
        public Delivery
        (DateTime orderDate, 
        DateTime deliveryDate, 
        OrderStatus orderStatus,
        int itemNumber,
        int itemQuantity,
        int customerId
        )
        {
            OrderDate = orderDate;
            DeliveryDate = deliveryDate;
            OrderStatus = orderStatus;
            ItemNumber = itemNumber;
            ItemQuantity = itemQuantity;
            CustomerId = customerId;

        }
        public int Id { get; set; }
        
      public DateTime OrderDate { get; set; }
      public DateTime DeliveryDate { get; set; }
      public OrderStatus OrderStatus { get; set; }
      public int ItemNumber { get; set; }
      public int ItemQuantity { get; set; }
      public int CustomerId { get; set; }
    }
}