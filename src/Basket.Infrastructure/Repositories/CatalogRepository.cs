using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.Models;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CatalogRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("Catalog");

            try
            {
                var response = await client.GetAsync($"api/Product/{id}");
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;

                return JsonConvert.DeserializeObject<Product>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
