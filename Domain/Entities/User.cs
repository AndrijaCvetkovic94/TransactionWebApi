using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public string AccountNumber { get; set;} = string.Empty;

    [Required]
    [MaxLength(256)]
    public string Adress { get; set;} = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }

    [Required]
    public List<PaymentTransaction> Transactions { get; set;} = new List<PaymentTransaction>();
}