using GeekBurger.StoreCatalogs.Domain.Entities;
using GeekBurger.StoreCatalogs.Domain.Publishers;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace GeekBurger.StoreCatalogs.Infra.ServiceBusImpl
{
    public class ProductReadyPublisher : IProductReadyPublisher
    {
        private readonly ITopicClient _topicClient;
        public ProductReadyPublisher(ITopicClient client)
        {
            _topicClient = client;
        }

        public async Task Publish(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                await SendMessage(product);
            }
        }

        public async Task Publish(Product product)
        {
            await SendMessage(product);
        }

        private async Task SendMessage(Product product)
        {
            var message = CreateMessage(product);

            await _topicClient.SendAsync(message);

            await _topicClient.CloseAsync();
        }

        private static Message CreateMessage(Product product)
        {
            return new Message(Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(product)));
        }
    }
}
