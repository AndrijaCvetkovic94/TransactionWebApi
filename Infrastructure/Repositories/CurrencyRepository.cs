using Infrastructure.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly AppDbContext _context;
    
    public CurrencyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Currency> GetCurrencyAsync(string CurrencyCode)
    {
        return await _context.Currencies.FindAsync(CurrencyCode);
    }
}