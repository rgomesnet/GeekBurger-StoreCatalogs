namespace GeekBurger.StoreCatalogs.Domain.Entities
{
    public record Store
    {
        public Guid StoreId { get; init; }
        public string Name { get; init; } = default!;
    }
}
