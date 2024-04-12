using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Currency
{
    [Key]
    [StringLength(3)]
    public string Code { get; set; }
}