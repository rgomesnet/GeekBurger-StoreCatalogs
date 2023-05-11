using GeekBurger.StoreCatalogs.Domain.Subscribers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;

namespace GeekBurger.StoreCatalogs.Infra.ServiceBusImpl
{
    public class ServiceBusService<TModel> : BackgroundService
    {
        private readonly IMessageHandler<TModel> _messageHandler;
        private readonly ISubscriptionClient _subscriptionClient;

        public ServiceBusService(
            IMessageHandler<TModel> messageHandler,
            ISubscriptionClient subscriptionClient)
        {
            _messageHandler = messageHandler;
            _subscriptionClient = subscriptionClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var model = Convert(message);
            await _messageHandler.HandleMessageAsync(model);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static TModel? Convert(Message message)
        {
            return JsonSerializer.Deserialize<TModel>(
                           Encoding.UTF8.GetString(message.Body),
                           new JsonSerializerOptions
                           {
                               PropertyNameCaseInsensitive = true
                           });
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine($"Message handler encountered an exception: {args.Exception}");
            return Task.CompletedTask;
        }
    }
}
