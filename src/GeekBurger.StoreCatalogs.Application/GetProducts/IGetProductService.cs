namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public interface IGetProductService
    {
        Task<IEnumerable<ProductToGet>> GetProductsByStoreName(string storeName);
        Task<IEnumerable<ProductToGet>> GetProducts(string storeName, int userId, string[] restrictions);
    }
}
