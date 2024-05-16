﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.30");

            modelBuilder.Entity("Domain.Entities.Currency", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Domain.Entities.MoneyWithdrawalFromUsersBalance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("MoneyWithdrawalCurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MoneyWithdrawalUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MoneyWithdrawalCurrencyCode");

                    b.HasIndex("MoneyWithdrawalUserId");

                    b.ToTable("MoneyWithdrawals");
                });

            modelBuilder.Entity("Domain.Entities.PaymentTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionCurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TransactionCurrencyCode");

                    b.HasIndex("TransactionUserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.MoneyWithdrawalFromUsersBalance", b =>
                {
                    b.HasOne("Domain.Entities.Currency", "MoneyWithdrawalCurrency")
                        .WithMany()
                        .HasForeignKey("MoneyWithdrawalCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "MoneyWithdrawalUser")
                        .WithMany("MoneyWithdrawals")
                        .HasForeignKey("MoneyWithdrawalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MoneyWithdrawalCurrency");

                    b.Navigation("MoneyWithdrawalUser");
                });

            modelBuilder.Entity("Domain.Entities.PaymentTransaction", b =>
                {
                    b.HasOne("Domain.Entities.Currency", "TransactionCurrency")
                        .WithMany()
                        .HasForeignKey("TransactionCurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "TransactionUser")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionCurrency");

                    b.Navigation("TransactionUser");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("MoneyWithdrawals");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
