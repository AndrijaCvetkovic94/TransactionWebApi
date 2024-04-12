using Domain.Entities;

namespace Domain.Interfaces;

public interface IPaymentTransactionRepository
{
    Task AddTransactionAsync(PaymentTransaction transaction);
}