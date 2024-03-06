using Basket.Domain.Models;

namespace Basket.Domain.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<Domain.Models.Basket> GetByUserId(string userId);
        Task AddItemToBasket(string userId, Item item);
        Task RemoveItemFromBasket(string userId, int itemId);
    }
}
