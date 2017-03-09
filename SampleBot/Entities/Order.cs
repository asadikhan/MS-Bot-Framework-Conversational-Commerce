using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBot.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime Date { get; set;  }
        public double Total { get; set;  }
        public string CustomerName { get; set; }
        public OrderStatusEnum Status { get; set;  }

        public string TrackingNumber { get; set; }

        public Order(string orderId, DateTime orderDate, double orderTotal, string customerName, OrderStatusEnum orderStatus, string trackingNumber)
        {
            Id = orderId;
            Date = orderDate;
            Total = orderTotal;
            CustomerName = customerName;
            Status = orderStatus;
            TrackingNumber = trackingNumber;
        }
    }

    public enum OrderStatusEnum {
        Picking, 
        Shipped, 
        Delivered, 
        Cancelled,
        PendingReturn, 
        Returned
    }
}