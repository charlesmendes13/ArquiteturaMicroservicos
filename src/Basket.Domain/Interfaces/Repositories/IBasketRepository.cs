using Basket.Domain.Models;

namespace Basket.Domain.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<IEnumerable<Item>> GetListItemByUserId(string userId);
        Task AddItemToBasket(string userId, Item item);
        Task RemoveItemFromBasket(string userId, int itemId);
    }
}
