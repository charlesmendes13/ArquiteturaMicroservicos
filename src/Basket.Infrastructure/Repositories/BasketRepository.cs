using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.Models;
using Basket.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly BasketContext _context;

        public BasketRepository(BasketContext context)
        {
            _context = context;
        }        

        public async Task<Domain.Models.Basket> GetByUserId(string userId)
        {
            try
            {
                return await _context.Basket
                    .Include(x => x.Items)
                    .Where(x => x.UserId == userId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddItemToBasket(string userId, Item item)
        {
            try
            {
                // Verificar se o usuário já possui um carrinho
                var basket = _context.Basket
                    .Include(b => b.Items)
                    .FirstOrDefault(b => b.UserId == userId);

                if (basket == null)
                {
                    // Se o usuário não tiver um carrinho, crie um novo carrinho
                    basket = new Domain.Models.Basket
                    {
                        UserId = userId,
                        Items = new List<Item>()
                    };
                    _context.Basket.Add(basket);
                }

                // Verificar se o item já existe no carrinho
                var item_ = basket.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

                if (item_ != null)
                {
                    // Se o item já existir, apenas atualize a quantidade
                    item_.Quantity += item.Quantity;
                }
                else
                {
                    // Se o item não existir, crie um novo item e adicione ao carrinho
                    basket.Items.Add(new Item()
                    {
                        Quantity = item.Quantity,
                        ProductId = item.ProductId,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }            
        }        

        public async Task RemoveItemFromBasket(string userId, int itemId)
        {
            try
            {
                // Verificar se o usuário já possui um carrinho
                var basket = _context.Basket
                    .Include(b => b.Items)
                    .FirstOrDefault(b => b.UserId == userId);

                if (basket != null)
                {
                    // Encontre o item no carrinho
                    var item = basket.Items.FirstOrDefault(i => i.Id == itemId);

                    if (item != null)
                    {
                        // Remove a Quantidade do Item
                        if (item.Quantity > 1)
                        {
                            item.Quantity -= 1;
                        }
                        else
                        {
                            // Remove o Item
                            basket.Items.Remove(item);
                            _context.Item.Remove(item);
                        }

                        // Verifique se o carrinho está vazio
                        if (basket.Items.Count == 0)
                        {
                            _context.Basket.Remove(basket);
                        }

                        // Salve as alterações no banco de dados
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }        
    }
}
