using GeekBurger.StoreCatalogs.Domain.Entities;

namespace GeekBurger.StoreCatalogs.Domain.Repositories
{
    public interface IProductRepository
    {
        void Upsert(Product product);
        void Upsert(IEnumerable<Product> products);
        IEnumerable<Product> GetProductsByStoreNameAndProductIds(string storeName, IEnumerable<Guid> productIds);
    }
}
