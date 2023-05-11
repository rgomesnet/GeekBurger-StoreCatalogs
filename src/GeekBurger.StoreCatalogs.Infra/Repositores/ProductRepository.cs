using GeekBurger.StoreCatalogs.Domain.Entities;
using GeekBurger.StoreCatalogs.Domain.Repositories;

namespace GeekBurger.StoreCatalogs.Infra.Repositores
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public void Upsert(Product product)
        {
            _products.RemoveAll(p => product.ProductId.Equals(p.ProductId));
            _products.Add(product);
        }

        public void Upsert(IEnumerable<Product> products)
        {
            _products.RemoveAll(p => products.Any(pr => pr.ProductId == p.ProductId));
            _products.AddRange(products);
        }

        public IEnumerable<Product> GetProductsByStoreNameAndProductIds(string storeName, IEnumerable<Guid> productIds)
        {
            var products =
                _products.Where(p => p.Store.Name.Equals(storeName, StringComparison.CurrentCultureIgnoreCase));

            if (productIds.Any())
            {
                return products.Where(p => productIds.Contains(p.ProductId));
            }

            return products;
        }
    }
}
