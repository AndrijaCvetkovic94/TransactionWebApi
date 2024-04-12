using Domain.Entities;

namespace Domain.Interfaces;

public interface ITransactionRepository
{
    Task AddTransactionAsync(PaymentTransaction transaction);
}