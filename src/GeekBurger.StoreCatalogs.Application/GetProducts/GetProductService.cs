using GeekBurger.StoreCatalogs.Domain.Repositories;

namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IIngredientsClientService _ingredientsClientService;

        public GetProductService(IProductRepository productRepository, IIngredientsClientService ingredientsClientService)
            => (_productRepository, _ingredientsClientService) = (productRepository, ingredientsClientService);

        public async Task<IEnumerable<ProductToGet>> GetProducts(string storeName, int userId, IEnumerable<string> restrictions)
        {
            var productIds = Enumerable.Empty<Guid>();

            if (restrictions.Any())
            {
                var ingredientes =
                    await _ingredientsClientService.GetIngredientsByRestrictions(storeName, restrictions);

                productIds = ingredientes.Select(_ => _.ProductId);
            }

            var producst = _productRepository.GetProductsByStoreNameAndProductIds(storeName, productIds);

            return producst.Select(p => (ProductToGet)p).ToList();
        }
    }
}
