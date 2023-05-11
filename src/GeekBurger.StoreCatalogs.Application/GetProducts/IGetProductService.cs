namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public interface IGetProductService
    {
        Task<IEnumerable<ProductToGet>> GetProducts(string storeName, int userId, IEnumerable<string> restrictions);
    }
}
