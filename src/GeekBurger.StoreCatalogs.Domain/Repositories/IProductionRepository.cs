using GeekBurger.StoreCatalogs.Domain.Entities;

namespace GeekBurger.StoreCatalogs.Domain.Repositories
{
    public interface IProductionRepository
    {
        void Upsert(Production production);
    }
}
