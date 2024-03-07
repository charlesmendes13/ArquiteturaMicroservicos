using Basket.Domain.Models;

namespace Basket.Domain.Interfaces.Services
{
    public interface IBasketService
    {
        Task<Domain.Models.Basket> GetByUserIdAsync(string userId);
        Task AddItemToBasketAsync(string userId, Item item);
        Task RemoveItemFromBasketAsync(string userId, int itemId);
        Task DeleteByUserIdAsync(string userId);
    }
}
