using Infrastructure.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;
        
        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Currency> GetCurrencyAsync(string currencyCode, CancellationToken cancellationToken)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Code == currencyCode, cancellationToken);
        }
    }
}