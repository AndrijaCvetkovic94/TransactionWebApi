using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PaymentTransaction
{
    [Key]
    public Guid Id { get; set;}

    [Required]
    public DateTime Timestamp { get; set;}

    [Required]
    [ForeignKey("TransactionUser")]
    public int TransactionUserId { get; set;}
    public User TransactionUser { get; set;}

    [Required]
    [ForeignKey("TransactionCurrency")]
    public string TransactionCurrencyCode { get; set;}
    public Currency TransactionCurrency { get; set;}

    [Required]
    public int Amount { get; set;}

    [Required]
    [MaxLength(256)]
    public string Description { get; set;} = string.Empty;
}