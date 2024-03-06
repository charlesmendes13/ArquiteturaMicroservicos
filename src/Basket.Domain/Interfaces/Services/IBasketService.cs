using Basket.Domain.Models;

namespace Basket.Domain.Interfaces.Services
{
    public interface IBasketService
    {
        Task<IEnumerable<Item>> GetListItemByUserId(string userId);
        Task AddItemToBasket(string userId, Item item);
        Task RemoveItemFromBasket(string userId, int itemId);
    }
}
