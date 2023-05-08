using GeekBurger.StoreCatalogs.Domain.Entities;

namespace GeekBurger.StoreCatalogs.Domain.Repositories
{
    public interface IProductRepository
    {
        void Upsert(IEnumerable<Product> products);
        IEnumerable<Product> GetProductsByStoreName(string storeName);
    }
}
