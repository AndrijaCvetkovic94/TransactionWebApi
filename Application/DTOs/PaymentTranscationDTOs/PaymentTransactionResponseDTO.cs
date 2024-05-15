namespace Application.DTOs;

public class PaymentTransactionResponseDTO
{   
    public Guid? TransactionId { get; init;}
    public string Description { get; init;}
    public int Status { get; init; }
}