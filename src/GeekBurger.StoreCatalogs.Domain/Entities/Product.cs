namespace GeekBurger.StoreCatalogs.Domain.Entities
{
    public record Product
    {
        public Guid ProductId { get; init; }
        public Store Store { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Image { get; init; } = default!;
        public decimal Price { get; init; } = default!;
        public ICollection<Item> Items { get; init; } = default!;
    }
}
