using Infrastructure.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class PaymentTransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;
    
    public PaymentTransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTransactionAsync(PaymentTransaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }
}