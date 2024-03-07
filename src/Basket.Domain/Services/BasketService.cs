using Basket.Domain.Interfaces.Client;
using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.Interfaces.Services;
using Basket.Domain.Models;

namespace Basket.Domain.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly ICatalogClient _client;

        public BasketService(IBasketRepository repository,
            ICatalogClient client)
        {
            _repository = repository;
            _client = client;
        }
        
        public async Task<Models.Basket> GetByUserIdAsync(string userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task AddItemToBasketAsync(string userId, Item item)
        {
            var product = await _client.GetProductByIdAsync(item.ProductId);

            item.Name = product.Name;
            item.Description = product.Description;
            item.Price = product.Price;

            await _repository.AddItemToBasketAsync(userId, item);
        }        

        public async Task RemoveItemFromBasketAsync(string userId, int itemId)
        {
            await _repository.RemoveItemFromBasketAsync(userId, itemId);    
        }

        public async Task DeleteByUserIdAsync(string userId)
        {
            await _repository.DeleteByUserIdAsync(userId);
        }
    }
}
