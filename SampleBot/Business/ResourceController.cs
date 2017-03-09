using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Resources;

namespace SampleBot.Business
{
    public static class ResourceController
    {
        static ResourceManager rm = new ResourceManager("SampleBot.BotMessages", typeof(ResourceController).Assembly);

        public static string GetMessage(string title)
        {
            string value = rm.GetString(title);
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }

            return string.Empty;
        }
    }

    public static class MessageTitles
    {
        public const string Greeting = "Greeting";
        public const string CancelConfirm = "CancelConfirm";
        public const string CancelNotAllowed = "CancelNotAllowed";
        public const string CancelOrder = "CancelOrder";
        public const string CancelSuccessful = "CancelSuccessful";
        public const string CancelFailed = "CancelFailed";
        public const string Goodbye = "Goodbye";
        public const string OrderStatus = "OrderStatus";
        public const string ReturnConfirm = "ReturnConfirm";
        public const string ReturnNotAllowed = "ReturnNotAllowed";
        public const string ReturnOrder = "ReturnOrder";
        public const string ReturnSuccessful = "ReturnSuccessful";
        public const string ReturnFailed = "ReturnFailed";
        public const string TrackOrder = "TrackOrder";
        public const string OrderStatusConfirm = "OrderStatusConfirm";
        public const string TrackOrderConfirm = "TrackOrderConfirm";
        public const string AnythingElse = "AnythingElse";
        public const string Inconclusive = "Inconclusive";
        public const string ReturnNotConfirm = "ReturnNotConfirm";
        public const string CancelNotConfirm = "CancelNotConfirm";
    }

}