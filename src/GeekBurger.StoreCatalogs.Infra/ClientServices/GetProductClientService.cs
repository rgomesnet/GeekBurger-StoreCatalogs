using GeekBurger.StoreCatalogs.Application.GetProducts;
using System.Net.Http.Json;

namespace GeekBurger.StoreCatalogs.Infra.ClientServices
{
    public class GetProductClientService : IGetProductClientService
    {
        private readonly HttpClient _httpClient;
        public GetProductClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductsAPI");
        }

        public async Task<IEnumerable<ProductToGet>> GetProductsByStoreName(string storeName)
        {
            var productsToGet =
                await _httpClient.GetFromJsonAsync<IEnumerable<ProductToGet>>($"api/products?storeName={storeName}");

            return productsToGet ?? Enumerable.Empty<ProductToGet>();
        }
    }
}
