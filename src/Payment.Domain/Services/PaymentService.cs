using Payment.Domain.Interfaces.Client;
using Payment.Domain.Interfaces.EventBus;
using Payment.Domain.Interfaces.Repositories;
using Payment.Domain.Interfaces.Services;
using Payment.Domain.Models;

namespace Payment.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly IBasketClient _client;
        private readonly IOrderEventBus _eventBus;

        public PaymentService(IPaymentRepository repository,
            IBasketClient client,
            IOrderEventBus eventBus)
        {
            _repository = repository;
            _client = client;
            _eventBus = eventBus;
        }

        public async Task InsertAsync(Models.Payment payment)
        {
            var basket = await _client.GetBasketByUserIdAsync(payment.UserId);

            payment.BasketId = basket.Id;
            payment.Total = SumPriceItems(basket.Items);

            /*
                Aqui pode-se utilizar Provedores de Pagamento Externo
                para validar a compra. Mas como trata-se de fins acadêmicos, 
                vou tratar toda compra como Aprovada (var confirm = true) e
                criar uma Order.
            */

            var confirm = true;

            if (confirm) 
            {
                var payment_ = await _repository.InsertAsync(payment);

                if (payment_ != null)
                {
                    // Remove o Carrinho
                    await _client.DeleteBasketByUserIdAsync(payment.UserId);

                    // Cria a Order
                    await _eventBus.PublisherAsync(new Order()
                    {
                        PaymentId = payment.Id,
                        Total = payment.Total,
                        Basket = basket
                    });
                }
            }                        
        }

        #region private Methods

        private double SumPriceItems (List<Item> items)
        {
            double total = 0;

            foreach (var item in items) 
            {
                total += item.Price; 
            }

            return total;
        }

        #endregion
    }
}
