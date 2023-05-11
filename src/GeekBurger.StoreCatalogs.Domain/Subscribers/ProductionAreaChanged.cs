using GeekBurger.StoreCatalogs.Domain.Entities;

namespace GeekBurger.StoreCatalogs.Domain.Subscribers
{
    public record ProductionAreaChanged
    {
        public Guid ProductionId { get; init; }
        public IEnumerable<string> Restrictions { get; init; } = default!;
        public bool On { get; init; }

        public static implicit operator Production(ProductionAreaChanged productionAreaChanged)
        {
            return new Production
            {
                On = productionAreaChanged.On,
                Restrictions = productionAreaChanged.Restrictions,
                ProductionId = productionAreaChanged.ProductionId
            };
        }
    }
}
