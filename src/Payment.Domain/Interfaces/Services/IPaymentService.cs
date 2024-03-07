namespace Payment.Domain.Interfaces.Services
{
    public interface IPaymentService
    {
        Task InsertAsync(Models.Payment payment);
    }
}
