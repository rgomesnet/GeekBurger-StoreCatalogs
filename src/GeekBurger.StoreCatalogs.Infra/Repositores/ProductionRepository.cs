using GeekBurger.StoreCatalogs.Domain.Entities;
using GeekBurger.StoreCatalogs.Domain.Repositories;

namespace GeekBurger.StoreCatalogs.Infra.Repositores
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly List<Production> _productions = new();

        public void Upsert(Production production)
        {
            _productions.RemoveAll(p => production.ProductionId.Equals(p.ProductionId));
            _productions.Add(production);
        }
    }
}
