using SampleBot.Business;
using SampleBot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBot
{
    public static class PrepareSampleOrders
    {
        public static void Do()
        {
            Order o1 = new Order("OR1001", DateTime.Now, 45.99, "Derick Johnson", OrderStatusEnum.Picking, null);
            OrderManager.orders.Add(o1);

            Order o2 = new Order("OR1034", DateTime.Now.AddDays(-95), 99.99, "Steve Macoy", OrderStatusEnum.Delivered, "QTekPjVGuVlxAz0z");
            OrderManager.orders.Add(o2);

            Order o3 = new Order("OR1056", DateTime.Now.AddDays(-4), 33.99, "John Ives", OrderStatusEnum.Shipped, "8Um9JaswBw1BiLNx");
            OrderManager.orders.Add(o3);

            Order o4 = new Order("OR1123", DateTime.Now.AddDays(-20), 400.01, "Smith Conners", OrderStatusEnum.Delivered, "h5LbR1tknPgPUgiZ");
            OrderManager.orders.Add(o4);

            Order o5 = new Order("OR1199", DateTime.Now.AddDays(-1), 5.99, "James Paterson", OrderStatusEnum.Cancelled, null);
            OrderManager.orders.Add(o5);

            Order o6 = new Order("OR1432", DateTime.Now.AddDays(-30), 99.99, "Steven Johnson", OrderStatusEnum.Returned, null);
            OrderManager.orders.Add(o6);
        }

    }
}