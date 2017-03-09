using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBot.Entities
{
    public static class ConversationState
    {
        public const string GreetingSent = "GreetingSent"; 
        public const string OrderIntent = "OrderIntent";
        public const string OrderNumber = "OrderNumber" ; 
    }

    public static class OrderIntents {
        public const string Status = "Status";
        public const string Track = "Track";
        public const string Cancel = "Cancel";
        public const string Return = "Return";
    };
}