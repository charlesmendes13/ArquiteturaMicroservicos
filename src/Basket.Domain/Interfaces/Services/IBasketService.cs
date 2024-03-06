using Basket.Domain.Models;

namespace Basket.Domain.Interfaces.Services
{
    public interface IBasketService
    {
        Task AddItemToBasket(string userId, Item item);
        Task RemoveItemFromBasket(string userId, int itemId);
    }
}
