using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MoneyWithdrawalFromUsersBalance
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("MoneyWithdrawalUser")]
    public int MoneyWithdrawalUserId { get; set; }
    public User MoneyWithdrawalUser { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    [ForeignKey("MoneyWithdrawalCurrency")]
    public string MoneyWithdrawalCurrencyCode { get; set; }
    public Currency MoneyWithdrawalCurrency { get; set; }

    [Required]
    [MaxLength(256)]
    public string Description { get; set; } = string.Empty;

    public MoneyWithdrawalFromUsersBalance()
    {

    }

    public MoneyWithdrawalFromUsersBalance(Guid id,
        DateTime timeStamp,
        decimal amount,
        Currency moneyWithdrawalCurrency,
        User moneyWithdrawalUser,
        string description)
    {
        Id = id;
        Timestamp = timeStamp;
        Amount = amount;
        MoneyWithdrawalCurrency = moneyWithdrawalCurrency;
        MoneyWithdrawalUser = moneyWithdrawalUser;
        Description = description;
    }
}
