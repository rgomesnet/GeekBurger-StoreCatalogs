using GeekBurger.StoreCatalogs.Application.GetProducts;
using System.Text.Json;

namespace GeekBurger.StoreCatalogs.Infra.ClientServices
{
    public class IngredientsClientService : IIngredientsClientService
    {
        private readonly HttpClient _httpClient;
        public IngredientsClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("IngredientssAPI");
        }

        public async Task<IEnumerable<IngredientsToGet>> GetIngredientsByRestrictions(string storeName, IEnumerable<string> restrictions)
        {
            try
            {
                var request = CreateRequest(restrictions);
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var productsToGet = await SerializeToProductsToGet(response);

                return productsToGet is not null ? productsToGet : Enumerable.Empty<IngredientsToGet>();
            }
            catch
            {
                return Enumerable.Empty<IngredientsToGet>();
            }
        }

        private static async Task<IEnumerable<IngredientsToGet>?> SerializeToProductsToGet(HttpResponseMessage response)
        {
            return (await JsonSerializer.DeserializeAsync<IngredientsToGet[]>((await response.Content.ReadAsStreamAsync()), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }));
        }

        private static HttpRequestMessage CreateRequest(IEnumerable<string> restrictions)
        {
            var bodyRequest = JsonSerializer.Serialize(new
            {
                StoreName = "Paulista",
                Restrictions = restrictions
            });

            var request = new HttpRequestMessage(HttpMethod.Post, "api/products/byrestrictions");
            request.Headers.Add("accept", "text/plain");

            var content = new StringContent(bodyRequest, null, "application/json");
            request.Content = content;
            return request;
        }
    }
}
