using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityConfigurations;

public class MoneyWithdrawalFromUsersBalanceEntityConfiguration : IEntityTypeConfiguration<MoneyWithdrawal>
{
    public void Configure(EntityTypeBuilder<MoneyWithdrawal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Timestamp).IsRequired();
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.Description).HasMaxLength(256).IsRequired();

        builder.HasOne(x => x.MoneyWithdrawalUser)
            .WithMany(u => u.MoneyWithdrawals)
            .HasForeignKey(x => x.MoneyWithdrawalUser)
            .IsRequired();

        builder.HasOne(x => x.MoneyWithdrawalCurrency)
            .WithMany()
            .HasForeignKey(x => x.MoneyWithdrawalCurrencyCode)
            .IsRequired();

    }
}
