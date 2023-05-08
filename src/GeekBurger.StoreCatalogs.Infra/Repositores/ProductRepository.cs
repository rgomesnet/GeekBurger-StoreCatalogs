using GeekBurger.StoreCatalogs.Domain.Entities;
using GeekBurger.StoreCatalogs.Domain.Repositories;

namespace GeekBurger.StoreCatalogs.Infra.Repositores
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();
        public void Upsert(IEnumerable<Product> products)
        {
            _products.RemoveAll(p => products.Any(pr => pr.ProductId == p.ProductId));
            _products.AddRange(products);
        }

        public IEnumerable<Product> GetProductsByStoreName(string storeName)
            => _products.Where(p => p.Store.Name.Equals(storeName, StringComparison.CurrentCultureIgnoreCase));
    }
}
