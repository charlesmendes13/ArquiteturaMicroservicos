using Basket.Domain.Models;

namespace Basket.Domain.Interfaces.Services
{
    public interface ICatalogService
    {
        Task<Product> GetProductByIdAsync(int id);
    }
}
