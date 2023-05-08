using GeekBurger.StoreCatalogs.Domain.Entities;

namespace GeekBurger.StoreCatalogs.Domain.Repositories
{
    public interface IStoreRepository
    {
        void Insert(Store newstore);
        Task<Store?> GetStoreById(Guid id);
        Task<Store?> GetStoreByName(string name);
    }
}
