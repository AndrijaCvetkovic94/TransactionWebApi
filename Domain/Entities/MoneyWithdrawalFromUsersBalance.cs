using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MoneyWithdrawalFromUsersBalance
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("MoneyWithdrawalUser")]
    public int MoneyWithdrawalUserId { get; private set; }
    public User MoneyWithdrawalUser { get; private set; }

    [Required]
    public DateTime Timestamp { get; private set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; private set; }

    [Required]
    [ForeignKey("MoneyWithdrawalCurrency")]
    public string MoneyWithdrawalCurrencyCode { get; private set; }
    public Currency MoneyWithdrawalCurrency { get; private set; }

    [Required]
    [MaxLength(256)]
    public string Description { get; private set; } = string.Empty;

    public MoneyWithdrawalFromUsersBalance(Guid id, 
        DateTime timeStamp, 
        decimal amount, 
        Currency moneyWithdrawalFromUsersBalanceCurrency, 
        User moneyWithdrawalFromUsersBalanceUser, 
        string description)
    {
        Id = id;
        Timestamp = timeStamp;
        Amount = amount;
        MoneyWithdrawalCurrency = moneyWithdrawalFromUsersBalanceCurrency;
        MoneyWithdrawalUser = moneyWithdrawalFromUsersBalanceUser;
        Description = description;
    }
}
