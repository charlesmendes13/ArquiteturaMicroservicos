using Order.Domain.Interfaces.Client;
using Order.Domain.Interfaces.Proxys;
using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Services;

namespace Order.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IIdentityClient _client;
        private readonly IEmailProxy _emailProxy;

        public OrderService(IOrderRepository repository,
            IIdentityClient client,
            IEmailProxy emailProxy)
        {
            _repository = repository;
            _client = client;
            _emailProxy = emailProxy;
        }

        public async Task<bool> CreateOrderAsync(Models.Order order)
        {
            await _repository.InsertAsync(order);

            var user = await _client.GetUserByUserId(order.Basket.UserId);

            if (user != null) 
            {
                await CreateConfirmationOrderAsync(order, user.Email);
            }

            return true;
        }

        #region private Methods

        private async Task CreateConfirmationOrderAsync(Models.Order order, string email)
        {
            var text = "Obriogado por realizar sua compra!\n";
            text += "\nLista de Itens do Pedido:\n";

            foreach (var item in order.Basket.Items)
            {
                text += $"{item.Name}: Descricao: {item.Description} R$ {item.Price}\n";
            }

            text += $"\nTotal da compra: R$ {order.Total}\n";          

            await _emailProxy.SendAsync("no-replay@infnet.com",
                email,
                "Confirmation Order Infnet",
                text);
        }

        #endregion
    }
}
