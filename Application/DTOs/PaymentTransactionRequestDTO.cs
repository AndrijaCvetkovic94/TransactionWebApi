namespace Application.DTOs;

public class PaymentTransactionRequestDTO
{
    public Guid TransactionId { get; init;}
    public int UserId { get; init;}
    public string Currency {get; init;}
    public decimal Amount { get; init;}
}