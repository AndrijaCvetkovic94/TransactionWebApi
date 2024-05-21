using Domain.Entities;

namespace Domain.Interfaces;

public interface IMoneyWithdrawalRepository
{
    Task AddMoneyWithdrawalAsync(MoneyWithdrawal moneyWithdrawal, CancellationToken cancellationToken);
}
