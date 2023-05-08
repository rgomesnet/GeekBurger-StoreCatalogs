namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public interface IGetProductClientService
    {
        Task<IEnumerable<ProductToGet>> GetProductsByStoreName(string storeName);
    }
}
