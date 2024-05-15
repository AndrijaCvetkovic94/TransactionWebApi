using Infrastructure.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class MoneyWithdrawalFromUsersBalanceRepository : IMoneyWithdrawalFromUsersBalanceRepository
{
    private readonly AppDbContext _context;

    public MoneyWithdrawalFromUsersBalanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddMoneyWithdrawalAsync(MoneyWithdrawalFromUsersBalance moneyWithdrawal, CancellationToken cancellationToken)
    {
        await _context.MoneyWithdrawals.AddAsync(moneyWithdrawal, cancellationToken);
    }
}
