using GeekBurger.StoreCatalogs.Domain.Repositories;

namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _productRepository;

        public GetProductService(IProductRepository productRepository)
            => _productRepository = productRepository;

        public async Task<IEnumerable<ProductToGet>> GetProductsByStoreName(string storeName)
        {
            await Task.CompletedTask;
            return Enumerable.Empty<ProductToGet>();
        }

        public async Task<IEnumerable<ProductToGet>> GetProducts(string storeName, int userId, string[] restrictions)
        {
            await Task.CompletedTask;
            var producst = _productRepository.GetProductsByStoreName(storeName);
            return producst.Select(p => (ProductToGet)p).ToList();
        }
    }
}
