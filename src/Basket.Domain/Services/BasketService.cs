using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.Interfaces.Services;
using Basket.Domain.Models;

namespace Basket.Domain.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly ICatalogService _catalogService;

        public BasketService(IBasketRepository repository, 
            ICatalogService catalogService)
        {
            _repository = repository;
            _catalogService = catalogService;
        }

        public async Task<IEnumerable<Item>> GetListItemByUserId(string userId)
        {
            return await _repository.GetListItemByUserId(userId);
        }

        public async Task AddItemToBasket(string userId, Item item)
        {
            var product = await _catalogService.GetProductByIdAsync(item.ProductId);

            item.Name = product.Name;
            item.Description = product.Description;
            item.Price = product.Price;

            await _repository.AddItemToBasket(userId, item);
        }        

        public async Task RemoveItemFromBasket(string userId, int itemId)
        {
            await _repository.RemoveItemFromBasket(userId, itemId);    
        }
    }
}
