using Microsoft.EntityFrameworkCore;
using Payment.Domain.Interfaces.Repositories;
using Payment.Infrastructure.Context;

namespace Payment.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
        }

        public async Task<Domain.Models.Payment> InsertAsync(Domain.Models.Payment payment)
        {
            try
            {
                var payment_ = await _context.Payment
                    .FirstOrDefaultAsync(x => x.BasketId == payment.BasketId);

                if (payment_ != null)
                    return null;

                await _context.Payment.AddAsync(payment);
                await _context.SaveChangesAsync();

                return payment;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
