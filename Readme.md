# Conversation Commerce Bot Handling Order Management
This is a chat bot built using Microsoft Bot Framework using C# and .NET. It uses [LUIS.ai](https://www.luis.ai) for Natural Language Processing. It also uses Bot State Service for keeping state context between each message. You can find more information about the framework [here](https://docs.botframework.com/en-us/csharp/builder/sdkreference/index.html). 

## Download the Microsoft Bot Emulator
To see the chat bot in action right away, you can download the source code from this github and then download the [Microsoft Bot Emulator](https://docs.botframework.com/en-us/csharp/builder/sdkreference/gettingstarted.html) from [here](https://github.com/Microsoft/BotFramework-Emulator#download). 

## Try It Out
This solution was built using Visual Studio 2015. Once you download the source code, you can compile and run it to localhost. In the emulator "Bot Url" field, you can type your localhost URI with port e.g. "http://localhost:3979/api/messages" to be able to talk to the bot. 

## Deploy and Use on Various Channels including Skype, Facebook, Slack, etc.
If you like you can also register at [https://dev.botframework.com](https://dev.botframework.com) and click on Register a Bot. This will give you the followig two keys that you can then plug into the web.config of your application. 

```sh 
<add key="MicrosoftAppId" value="" />
<add key="MicrosoftAppPassword" value="" />
```

You can read more about authentication and deployment [here](https://docs.botframework.com/en-us/support/troubleshooting-bot-framework-authentication/). 

## Solution Overview


![alt tag](https://github.com/asadikhan/MS-Bot-Framework-Conversational-Commerce/blob/master/SampleBot/images/SolutionOverview.png)

Controller
	/MessagesController.cs *This contains the main class responsible for the conversation. The entry point for each message is the following post message. *

```sh 
public virtual async Task<HttpResponseMessage> Post([FromBody]Activity activity)
```
							*MessageController also contains the LuisDialog class which has all the methods mapped to the LUIS intents. *
							
	/StateController.cs		*This class is not a controller in the MVC sense, but rather has only two methods for saving and getting conversation level data. This helps you track context of a conversation. In our solution, we use this to track what is the current intent of the user. There is also User level data state and Private Conversation state. We didn't use those in our solution, but you can read more about Bot state service [here](https://docs.botframework.com/en-us/csharp/builder/sdkreference/stateapi.html). 
	
Entities
	/ConversationState.cs 	*A static class to help drive conversation state with strongly-typed strings.*
	/Order.cs				*A sample order entity class with some basic properties*
	
App_Start
	/PrepareSampleOrders.cs	*This init class loads a few sample orders for the business layer*. 
	
Business
	/OrderManager.cs		*This is a stub class that has some basic functions to fullfil order management like cancel or return an order, etc. In real world, this would be your integration with the order business layer.*
	ResourceController.cs	*To avoid polluting MessagesController.cs with text messages, this class is responsible for reading the messages out of the BotMessages.resx and sending them to the MessageController calling class. This can also help in future with multi-language support. Any messages to be changed are simply done in the .resx file, and no code compile is necessary. *
	
BotMessages.resx			*Resource file containing message replies to be sent by the bot. 

## More Information

There will be a more detailed tutorial on this solution on my blog. This space will be updated as soon as the tutorial becomes available on my [blog](https://asadkhanonline.wordpress.com/)