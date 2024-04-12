namespace Application.DTOs;

public class PaymentTransactionRequestDTO
{
    public Guid TransactionId { get; set;}
    public int UserId { get; set;}
    public string Currency {get; set;} = string.Empty;
    public int Amount { get; set;}
}