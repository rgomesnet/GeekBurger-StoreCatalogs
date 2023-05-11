using GeekBurger.StoreCatalogs.Domain.Entities;

namespace GeekBurger.StoreCatalogs.Domain.Publishers
{
    public interface IProductReadyPublisher
    {
        Task Publish(Product product);
        Task Publish(IEnumerable<Product> products);
    }
}
