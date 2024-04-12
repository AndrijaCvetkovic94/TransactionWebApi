namespace Application.DTOs;

public class PaymentTransactionResponseDTO
{   
    public Guid TransactionId { get; set;}
    public string Description { get; set;}
    public int Status { get; set;}
}