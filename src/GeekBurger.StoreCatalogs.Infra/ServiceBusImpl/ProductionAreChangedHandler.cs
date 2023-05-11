using GeekBurger.StoreCatalogs.Domain.Entities;
using GeekBurger.StoreCatalogs.Domain.Repositories;
using GeekBurger.StoreCatalogs.Domain.Subscribers;

namespace GeekBurger.StoreCatalogs.Infra.ServiceBusImpl
{
    public class ProductionAreChangedHandler : IProductionAreChangedHandler
    {
        private readonly IProductionRepository _productionRepository;

        public ProductionAreChangedHandler(IProductionRepository productionRepository)
        {
            _productionRepository = productionRepository;
        }
        public async Task HandleMessageAsync(ProductionAreaChanged message)
        {
            if (message is null)
            {
                await Console.Out.WriteLineAsync("productionAreaChanged is null}");
                return;
            }

            Production production = message;
            _productionRepository.Upsert(production);
            await Console.Out.WriteLineAsync(message!.ProductionId.ToString());
        }
    }
}