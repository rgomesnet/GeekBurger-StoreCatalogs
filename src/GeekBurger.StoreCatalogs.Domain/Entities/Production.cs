namespace GeekBurger.StoreCatalogs.Domain.Entities
{
    public record Production
    {
        public bool On { get; init; }
        public Guid ProductionId { get; init; }
        public IEnumerable<string> Restrictions { get; init; } = default!;
    }
}
