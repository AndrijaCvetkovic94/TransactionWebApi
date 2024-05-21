using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities;
using System.Net.NetworkInformation;
using Infrastructure.Data.EntityConfigurations;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentTransaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<MoneyWithdrawal> MoneyWithdrawals { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTransactionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MoneyWithdrawalFromUsersBalanceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyEntityConfiguration());

        }
    }
}