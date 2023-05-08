using GeekBurger.StoreCatalogs.Domain.Entities;

namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public record ProductToGet
    {
        public Guid StoreId { get; init; }
        public Guid ProductId { get; init; }
        public string Name { get; init; } = default!;
        public string Image { get; init; } = default!;
        public decimal Price { get; init; } = default!;
        public List<ItemToGet> Items { get; init; } = new();

        public static implicit operator ProductToGet(Product p)
        {
            return new ProductToGet
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Image = p.Image,
                Price = p.Price,
                StoreId = p.Store.StoreId,
                Items = p.Items?.Select(i => new ItemToGet
                {
                    ItemId = i.ItemId,
                    Name = i.Name
                }).ToList() ?? default!
            };
        }

        public static implicit operator Product(ProductToGet p)
        {
            return new Product
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Image = p.Image,
                Price = p.Price,
                Store = new Store
                {
                    StoreId = p.StoreId
                },
                Items = p.Items?.Select(i => new Item
                {
                    ItemId = i.ItemId,
                    Name = i.Name
                }).ToList() ?? default!
            };
        }
    }
}
