using GeekBurger.StoreCatalogs.Application.GetProducts;
using GeekBurger.StoreCatalogs.Domain.Entities;
using GeekBurger.StoreCatalogs.Domain.Repositories;

namespace GeekBurger.StoreCatalogs.Middlewares
{
    public class Initialization
    {
        private readonly IProductRepository _productRepository;
        private readonly IGetProductClientService _getProductClientService;

        public Initialization(IGetProductClientService getProductClientService, IProductRepository productRepository)
            => (_getProductClientService, _productRepository) = (getProductClientService, productRepository);

        public async Task RunAsync()
        {
            var products = await _getProductClientService.GetProductsByStoreName("Paulista");
            var upsertProducts = CreateProducts(products, "Paulista");
            _productRepository.Upsert(upsertProducts);

            products = await _getProductClientService.GetProductsByStoreName("Morumbi");
            upsertProducts = CreateProducts(products, "Morumbi");
            _productRepository.Upsert(upsertProducts);
        }

        private static IEnumerable<Product> CreateProducts(IEnumerable<ProductToGet> products, string storeName)
        {
            return products.Select(p =>
            {
                var pr = (Product)p;
                return pr with
                {
                    Store = new Store
                    {
                        Name = storeName,
                        StoreId = p.StoreId
                    }
                };
            });
        }
    }
}