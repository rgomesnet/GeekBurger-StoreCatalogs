using Microsoft.Azure.ServiceBus;
using System.Text;

namespace GeekBurger.StoreCatalogs.Application.StoreCatalogReadyEvents
{
    public class StoreCatalogReadySubscriber
    {
        private readonly ISubscriptionClient _subscriptionClient;

        public StoreCatalogReadySubscriber(string serviceBusConnectionString, string topicName, string subscriptionName)
        {
            _subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topicName, subscriptionName);
            RegisterOnMessageHandlerAndReceiveMessages(_subscriptionClient!);
        }

        void RegisterOnMessageHandlerAndReceiveMessages(ISubscriptionClient subscriptionClient)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: {Encoding.UTF8.GetString(message.Body)}");

            // Determine the recipient based on the message content
            var recipient = GetRecipientFromMessage(message);

            //// Send the message to the appropriate destination
            //switch (recipient)
            //{
            //    case "destination1":
            //        await SendToDestination1Async(message);
            //        break;
            //    case "destination2":
            //        await SendToDestination2Async(message);
            //        break;
            //    default:
            //        Console.WriteLine($"Unknown recipient: {recipient}");
            //        break;
            //}

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        static string GetRecipientFromMessage(Message message)
        {
            // This is just an example. You would need to implement your own logic here to determine the recipient based on the message content.
            var messageText = Encoding.UTF8.GetString(message.Body);
            if (messageText.Contains("destination1"))
            {
                return "destination1";
            }
            else if (messageText.Contains("destination2"))
            {
                return "destination2";
            }
            else
            {
                return "unknown";
            }
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
    }
}
