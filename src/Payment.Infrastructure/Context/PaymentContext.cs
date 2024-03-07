using Microsoft.EntityFrameworkCore;

namespace Payment.Infrastructure.Context
{
    public class PaymentContext : DbContext, IDisposable
    {
        public virtual DbSet<Domain.Models.Payment> Payment { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
