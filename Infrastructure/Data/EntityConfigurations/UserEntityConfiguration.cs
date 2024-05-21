using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Adress).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Balance).HasColumnType("decimal(18,2)");
        builder.Property(x => x.AccountNumber).IsRequired();
        builder.HasIndex(x => x.AccountNumber).IsUnique();

        builder.HasMany(x => x.Transactions)
            .WithOne()
            .HasForeignKey(x => x.Id);

        builder.HasMany(x => x.MoneyWithdrawals)
            .WithOne()
            .HasForeignKey(x => x.Id);
    }
}
