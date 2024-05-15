using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; init; }

    [Required]
    [MaxLength(128)]
    public string Name { get; private set; } = string.Empty;
    
    [Required]
    [MaxLength(128)]
    public string LastName { get; private set; } = string.Empty;

    [Required]
    public string AccountNumber { get; private set;} = string.Empty;

    [Required]
    [MaxLength(256)]
    public string Adress { get; private set;} = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; private set; }

    [Required]
    public List<PaymentTransaction> Transactions { get; private set;} = new List<PaymentTransaction>();

    [Required]
    public List<MoneyWithdrawalFromUsersBalance> MoneyWithdrawals { get; private set; } = new List<MoneyWithdrawalFromUsersBalance>();

    public void AddMoneyToUsersAccount(decimal amount)
    {
        Balance += amount;
    }

    public void RemoveMoneyFromUsersAccount(decimal amount)
    {
        Balance -= amount;
    }


}