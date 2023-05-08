namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public record ItemToGet
    {
        public Guid ItemId { get; init; }
        public string Name { get; init; } = default!;
    }
}
