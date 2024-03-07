using Basket.Domain.Models;

namespace Basket.Domain.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<Domain.Models.Basket> GetByUserIdAsync(string userId);
        Task AddItemToBasketAsync(string userId, Item item);
        Task RemoveItemFromBasketAsync(string userId, int itemId);
        Task DeleteByUserIdAsync(string userId);
    }
}
