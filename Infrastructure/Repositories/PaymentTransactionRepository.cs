using Infrastructure.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly AppDbContext _context;
        
        public PaymentTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(PaymentTransaction transaction,CancellationToken cancellationToken)
        {
            await _context.Transactions.AddAsync(transaction,cancellationToken);
            //await _context.SaveChangesAsync(cancellationToken);
        }
    }
}