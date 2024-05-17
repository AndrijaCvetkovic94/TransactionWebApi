using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PaymentTransaction
{
    [Key]
    public Guid Id { get; init;}

    [Required]
    public DateTime Timestamp { get; private set; }

    [Required]
    [ForeignKey("TransactionUser")]
    public int TransactionUserId { get; private set; }
    public User TransactionUser { get; private set; }

    [Required]
    [ForeignKey("TransactionCurrency")]
    public string TransactionCurrencyCode { get; private set; }
    public Currency TransactionCurrency { get; private set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; private set; }

    [Required]
    [MaxLength(256)]
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