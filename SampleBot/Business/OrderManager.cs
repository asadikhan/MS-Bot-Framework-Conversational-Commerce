using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleBot.Entities;

namespace SampleBot.Business
{
    public static class OrderManager
    {
        public static List<Order> orders = new List<Order>();

        public static bool IsCancellable(string orderId)
        {
            var order = orders.Where(o => o.Id.Equals(orderId, StringComparison.InvariantCultureIgnoreCase));
            if (order.Count() < 1)
            {
                return false;
            }

            // Run some business validation rules to check if order is cancellable
            if (order.First().Date > DateTime.Now.AddDays(-15))
            {
                return true;
            }

            return false;
        }

        public static bool CancelOrder(string orderId)
        {
            var order = orders.Where(o => o.Id.Equals(orderId, StringComparison.InvariantCultureIgnoreCase));
            if (order.Count() < 1)
            {
                return false;
            }

            // Run some business validation rules to check if order is cancellable
            if (order.First().Date > DateTime.Now.AddDays(-15))
            {
                // Cancel the order and notify user
                order.First().Status = OrderStatusEnum.Cancelled;
                return true;
            }

            return false;
        }

        public static string OrderStatus(string orderId)
        {
            var order = orders.Where(o => o.Id.Equals(orderId, StringComparison.InvariantCultureIgnoreCase));
            if (order.Count() < 1)
            {
                return "OrderNotFound";
            }

            return order.First().Status.ToString();
        }

        public static string TrackOrder(string orderId)
        {
            var order = orders.Where(o => o.Id.Equals(orderId, StringComparison.InvariantCultureIgnoreCase));
            if (order.Count() < 1)
            {
                return "OrderNotFound";
            }

            // Call third party to check for and return tracking number.
            return order.First().TrackingNumber ?? "NoTrackingNumberFound";
        }

        public static bool IsReturnable(string orderId)
        {
            var order = orders.Where(o => o.Id.Equals(orderId, StringComparison.InvariantCultureIgnoreCase));
            if (order.Count() < 1)
            {
                return false;
            }

            // Run some business validation rules to check if order is cancellable
            if (order.First().Date > DateTime.Now.AddDays(-90))
            {
                return true;
            }

            return false;
        }

        public static bool ReturnOrder(string orderId)
        {
            var order = orders.Where(o => o.Id.Equals(orderId, StringComparison.InvariantCultureIgnoreCase));
            if (order.Count() < 1)
            {
                return false;
            }

            // Run some business validation rules to check if order is returnable
            if (order.First().Date > DateTime.Now.AddDays(-90))
            {
                order.First().Status = OrderStatusEnum.PendingReturn;
                return true;
            }

            return false;
        }
    }
}