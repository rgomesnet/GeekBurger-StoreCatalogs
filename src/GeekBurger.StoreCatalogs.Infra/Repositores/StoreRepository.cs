using GeekBurger.StoreCatalogs.Domain.Entities;
using GeekBurger.StoreCatalogs.Domain.Repositories;

namespace GeekBurger.StoreCatalogs.Infra.Repositores
{
    public class StoreRepository : IStoreRepository
    {
        private List<Store> _stores = new()
        {
            new Store
            {
                StoreId = Guid.NewGuid(),
                Name = "Paulista"
            }
        };

        public async Task<Store?> GetStoreById(Guid id)
        {
            var store =
                _stores.FirstOrDefault(s => s.StoreId == id);

            await Task.CompletedTask;

            return store;
        }

        public async Task<Store?> GetStoreByName(string name)
        {
            var store =
                _stores.FirstOrDefault(s =>
                    s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            await Task.CompletedTask;

            return store;
        }

        public void Insert(Store newstore)
        {
            _stores.Add(newstore);
        }
    }
}
