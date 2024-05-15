using Domain.Entities;

namespace Domain.Interfaces;

public interface IMoneyWithdrawalFromUsersBalanceRepository
{
    Task AddMoneyWithdrawalAsync(MoneyWithdrawalFromUsersBalance moneyWithdrawal, CancellationToken cancellationToken);
}
