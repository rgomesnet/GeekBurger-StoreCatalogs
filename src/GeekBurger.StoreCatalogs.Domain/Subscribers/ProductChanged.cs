using GeekBurger.StoreCatalogs.Domain.Entities;

namespace GeekBurger.StoreCatalogs.Domain.Subscribers
{
    public record ProductChanged
    {
        public string ProductState { get; init; }
        public Product Product { get; init; } = default!;

        public static implicit operator Product(ProductChanged productChanged)
        {
            return new Product();
        }
    }
}
