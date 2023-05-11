namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public record IngredientsToGet
    {
        public Guid ProductId { get; set; }
        public IEnumerable<string> Ingredients { get; set; } = default!;
    }
}
