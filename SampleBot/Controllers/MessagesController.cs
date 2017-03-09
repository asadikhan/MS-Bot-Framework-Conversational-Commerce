using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using SampleBot.Business;
using SampleBot.Entities;

namespace SampleBot
{
    [LuisModel("00f40941-0d02-4b91-aadc-ea843608f06d", "a3277d41871542edb69a738f6a94407d")]
    [Serializable]
    public class OrderAssistantDialog : LuisDialog<object>
    {

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await PostAsyncMethod(context, ResourceController.GetMessage(MessageTitles.Inconclusive));
            context.Wait(MessageReceived);
        }

        [LuisIntent("NoResponse")]
        public async Task NoResponse(IDialogContext context, LuisResult result)
        {
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            await PostAsyncMethod(context, ResourceController.GetMessage(MessageTitles.Greeting));
            context.Wait(MessageReceived);
        }

        [LuisIntent("OrderStatus")]
        public async Task OrderStatus(IDialogContext context, LuisResult result)
        {
            if (MeetsMinimumIntentScore(result))
            {
                StateController.SetConversationData(ConversationState.OrderIntent, OrderIntents.Status);
                await PostAsyncMethod(context, ResourceController.GetMessage(MessageTitles.OrderStatus));
                context.Wait(MessageReceived);
            }
            else
            {
                await None(context, result);
            }
        }

        [LuisIntent("IdentifyOrderNumber")]
        public async Task IdentifyOrderNumber(IDialogContext context, LuisResult result)
        {
            if (MeetsMinimumIntentScore(result) && result.Entities.Count > 0)
            {
                var orderNumber = result.Entities.Where(e => e.Type == ConversationState.OrderNumber).First().Entity;
                StateController.SetConversationData(ConversationState.OrderNumber, orderNumber);

                string message = string.Empty;
                switch (StateController.GetConversationData(ConversationState.OrderIntent))
                {
                    case OrderIntents.Status:
                        var orderStatus = OrderManager.OrderStatus(orderNumber);
                        message = String.Format(ResourceController.GetMessage(MessageTitles.OrderStatusConfirm), orderStatus);
                        break;
                    case OrderIntents.Track:
                        var trackingNumber = OrderManager.TrackOrder(orderNumber);
                        message = String.Format(ResourceController.GetMessage(MessageTitles.TrackOrderConfirm), trackingNumber);
                        break;
                    case OrderIntents.Cancel:
                        bool isCancellable = OrderManager.IsCancellable(orderNumber);
                        if (!isCancellable)
                        {
                            message = ResourceController.GetMessage(MessageTitles.CancelNotAllowed);
                        }
                        else
                        {
                            message = ResourceController.GetMessage(MessageTitles.CancelConfirm);
                        }
                        break;
                    case OrderIntents.Return:
                        bool isReturnable = OrderManager.IsReturnable(orderNumber);
                        if (!isReturnable)
                        {
                            message = ResourceController.GetMessage(MessageTitles.ReturnNotAllowed);
                        }
                        else
                        {
                            message = ResourceController.GetMessage(MessageTitles.ReturnConfirm);
                        }
                        break;
                }

                await PostAsyncMethod(context, message);
                context.Wait(MessageReceived);
            }
            else
            {
                await None(context, result);
            }
        }

        [LuisIntent("TrackOrder")]
        public async Task TrackOrder(IDialogContext context, LuisResult result)
        {
            if (MeetsMinimumIntentScore(result))
            {
                StateController.SetConversationData(ConversationState.OrderIntent, OrderIntents.Track);
                await PostAsyncMethod(context, ResourceController.GetMessage(MessageTitles.TrackOrder));
                context.Wait(MessageReceived);
            }
            else
            {
                await None(context, result);
            }
        }

        [LuisIntent("CancelOrder")]
        public async Task CancelOrder(IDialogContext context, LuisResult result)
        {
            if (MeetsMinimumIntentScore(result))
            {
                StateController.SetConversationData(ConversationState.OrderIntent, OrderIntents.Cancel);
                await PostAsyncMethod(context, ResourceController.GetMessage(MessageTitles.CancelOrder));
                context.Wait(MessageReceived);
            }
            else
            {
                await None(context, result);
            }
        }

        [LuisIntent("ReturnOrder")]
        public async Task ReturnOrder(IDialogContext context, LuisResult result)
        {
            if (MeetsMinimumIntentScore(result))
            {
                StateController.SetConversationData(ConversationState.OrderIntent, OrderIntents.Return);
                await PostAsyncMethod(context, ResourceController.GetMessage(MessageTitles.ReturnOrder));
                context.Wait(MessageReceived);
            }
            else
            {
                await None(context, result);
            }
        }

        [LuisIntent("Affirmative")]
        public async Task Affirmative(IDialogContext context, LuisResult result)
        {
            if (MeetsMinimumIntentScore(result))
            {
                var orderNumber = StateController.GetConversationData(ConversationState.OrderNumber);

                string message = "";
                if (StateController.GetConversationData(ConversationState.OrderIntent) == OrderIntents.Cancel)
                {
                    bool cancelled = OrderManager.CancelOrder(orderNumber);
                    if (cancelled)
                    {
                        message = String.Format(ResourceController.GetMessage(MessageTitles.CancelSuccessful), orderNumber);
                    }
                    else
                    {
                        message = String.Format(ResourceController.GetMessage(MessageTitles.CancelFailed), orderNumber);
                    }
                }
                else if (StateController.GetConversationData(ConversationState.OrderIntent) == OrderIntents.Return)
                {
                    bool returned = OrderManager.ReturnOrder(orderNumber);
                    if (returned)
                    {
                        message = String.Format(ResourceController.GetMessage(MessageTitles.ReturnSuccessful), orderNumber);
                    }
                    else
                    {
                        message = String.Format(ResourceController.GetMessage(MessageTitles.ReturnFailed), orderNumber);
                    }
                }

                await PostAsyncMethod(context, message);
                context.Wait(MessageReceived);
            }
            else
            {
                await None(context, result);
            }
        }

        [LuisIntent("Negative")]
        public async Task Negative(IDialogContext context, LuisResult result)
        {
            if (MeetsMinimumIntentScore(result))
            {
                var orderNumber = StateController.GetConversationData(ConversationState.OrderNumber);

                string message = "";
                if (StateController.GetConversationData(ConversationState.OrderIntent) == OrderIntents.Cancel)
                {                    
                    message = String.Format(ResourceController.GetMessage(MessageTitles.CancelNotConfirm), orderNumber);
                }
                else if (StateController.GetConversationData(ConversationState.OrderIntent) == OrderIntents.Return)
                {
                    message = String.Format(ResourceController.GetMessage(MessageTitles.ReturnNotConfirm), orderNumber);
                }

                if (!string.IsNullOrEmpty(message))
                {
                    await PostAsyncMethod(context, message);
                    context.Wait(MessageReceived);
                }
                else
                {
                    await None(context, result);
                }
            }
            else
            {
                await None(context, result);
            }
        }

        public bool MeetsMinimumIntentScore(LuisResult result)
        {
            if (result.Intents.Count > 0)
            {
                if (result.Intents[0].Score < 0.4)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task PostAsyncMethod(IDialogContext context, string message)
        {
            await context.PostAsync(message); 
        }
        public OrderAssistantDialog()
        {
        }

        public OrderAssistantDialog(ILuisService service)
            : base(service)
        {
        }
    }



    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public virtual async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            StateController.stateClient = activity.GetStateClient();
            StateController.channelId = activity.ChannelId;
            StateController.fromId = activity.From.Id;

            // check if activity is of type message
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new OrderAssistantDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}