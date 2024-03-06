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

        public async Task AddItemToBasket(string userId, Item item)
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
            var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem != null)
            {
                // Se o item já existir, apenas atualize a quantidade
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                // Se o item não existir, crie um novo item e adicione ao carrinho
                var newItem = new Item
                {
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };
                basket.Items.Add(newItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromBasket(string userId, int itemId)
        {
            // Encontre o carrinho do usuário
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
    }
}
