namespace GeekBurger.StoreCatalogs.Application.GetProducts
{
    public interface IIngredientsClientService
    {
        Task<IEnumerable<IngredientsToGet>> GetIngredientsByRestrictions(string storeName, IEnumerable<string> restrictions);
    }
}


