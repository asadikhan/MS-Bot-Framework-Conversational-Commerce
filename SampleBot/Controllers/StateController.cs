using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleBot
{
    public static class StateController
    {
        public static StateClient stateClient;
        public static string channelId;
        public static string fromId;

        public static async void SetConversationData(string key, string value)
        {
            BotData conversationData = await stateClient.BotState.GetConversationDataAsync(channelId, fromId);
            conversationData.SetProperty<string>(key, value);
            await stateClient.BotState.SetConversationDataAsync(channelId, fromId, conversationData);
        }

        public static string GetConversationData(string key)
        {
            BotData conversationData = stateClient.BotState.GetConversationData(channelId, fromId);
            return conversationData.GetProperty<string>(key);
        }
    }
}