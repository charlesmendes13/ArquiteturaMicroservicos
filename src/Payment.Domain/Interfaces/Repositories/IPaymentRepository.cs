namespace Payment.Domain.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<Models.Payment> InsertAsync(Models.Payment payment);
    }
}
