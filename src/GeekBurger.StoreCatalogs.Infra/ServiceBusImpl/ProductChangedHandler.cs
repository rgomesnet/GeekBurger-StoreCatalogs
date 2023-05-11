using GeekBurger.StoreCatalogs.Domain.Entities;
using GeekBurger.StoreCatalogs.Domain.Publishers;
using GeekBurger.StoreCatalogs.Domain.Repositories;
using GeekBurger.StoreCatalogs.Domain.Subscribers;

namespace GeekBurger.StoreCatalogs.Infra.ServiceBusImpl
{
    public class ProductChangedHandler : IProductChangedHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductReadyPublisher _productReadyPublisher;

        public ProductChangedHandler(IProductRepository productRepository, IProductReadyPublisher productReadyPublisher)
        {
            _productRepository = productRepository;
            _productReadyPublisher = productReadyPublisher;
        }

        public async Task HandleMessageAsync(ProductChanged? productChanged)
        {
            if (productChanged is null)
            {
                await Console.Out.WriteLineAsync("productChanged is null");
                return;
            }

            Product product = productChanged;
            _productRepository.Upsert(product);

            await _productReadyPublisher.Publish(product);
            await Console.Out.WriteLineAsync(product!.ProductId.ToString());
        }
    }
}
