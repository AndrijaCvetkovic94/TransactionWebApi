using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PaymentTransaction
{
    public Guid Id { get; init;}

    public DateTime Timestamp { get; private set; }

    public int TransactionUserId { get; private set; }

    public User TransactionUser { get; private set; }

    public string TransactionCurrencyCode { get; private set; }

    public Currency TransactionCurrency { get; private set; }

    public decimal Amount { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public PaymentTransaction()
    {

    }

    public PaymentTransaction(Guid id, DateTime timeStamp, decimal amount, Currency transactionCurrency, User transactionUser, string description)
    {
        Id = id;
        Timestamp = timeStamp;
        Amount = amount;
        TransactionCurrency = transactionCurrency;
        TransactionUser = transactionUser;
        Description = description;
    }
    
}