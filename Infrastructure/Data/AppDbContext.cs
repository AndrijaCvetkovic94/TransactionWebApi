using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities;
using System.Net.NetworkInformation;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<PaymentTransaction> Transactions { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PaymentTransaction>()
            .HasOne(p => p.TransactionUser)
            .WithMany(u => u.Transactions)  // Link to the navigation property
            .HasForeignKey(p => p.TransactionUserId)
            .IsRequired();

        modelBuilder.Entity<PaymentTransaction>()
            .HasOne(t => t.TransactionCurrency)
            .WithMany()  // Update this if Currency has a collection of PaymentTransactions
            .HasForeignKey(t => t.TransactionCurrencyCode)
            .IsRequired();

        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Code)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.AccountNumber)
            .IsUnique();
    }
}