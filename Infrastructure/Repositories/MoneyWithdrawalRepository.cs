using Infrastructure.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class MoneyWithdrawalRepository : IMoneyWithdrawalRepository
{
    private readonly AppDbContext _context;

    public MoneyWithdrawalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddMoneyWithdrawalAsync(MoneyWithdrawal moneyWithdrawal, CancellationToken cancellationToken)
    {
        await _context.MoneyWithdrawals.AddAsync(moneyWithdrawal, cancellationToken);
    }
}
