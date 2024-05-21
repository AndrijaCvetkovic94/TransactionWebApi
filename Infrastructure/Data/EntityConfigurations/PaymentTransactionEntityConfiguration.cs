using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityConfigurations;

public class PaymentTransactionEntityConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Timestamp).IsRequired();
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description).IsRequired().HasMaxLength(256);

        builder.HasOne(x => x.TransactionUser)
            .WithMany(u => u.Transactions)
            .HasForeignKey(x => x.TransactionUserId)
            .IsRequired();

        builder.HasOne(x => x.TransactionCurrency)
            .WithMany()
            .HasForeignKey(x => x.TransactionCurrencyCode)
            .IsRequired();
    }
}
