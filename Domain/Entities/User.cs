using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(128)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string BankAccount { get; set;} = string.Empty;

    [Required]
    [MaxLength(256)]
    public string Adress { get; set;} = string.Empty;

    [Required]
    public List<PaymentTransaction> Transactions { get; set;} = new List<PaymentTransaction>();
}