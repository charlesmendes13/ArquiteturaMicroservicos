using Basket.Domain.Models;

namespace Basket.Domain.Interfaces.Repositories
{
    public interface ICatalogRepository
    {
        Task<Product> GetProductByIdAsync(int id);
    }
}
