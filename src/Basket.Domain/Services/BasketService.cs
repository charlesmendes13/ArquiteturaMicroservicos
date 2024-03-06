using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.Interfaces.Services;
using Basket.Domain.Models;

namespace Basket.Domain.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;

        public BasketService(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task AddItemToBasket(string userId, Item item)
        {
            await _repository.AddItemToBasket(userId, item);
        }

        public async Task RemoveItemFromBasket(string userId, int itemId)
        {
            await _repository.RemoveItemFromBasket(userId, itemId);    
        }
    }
}
