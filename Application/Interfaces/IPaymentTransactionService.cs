using Application.DTOs;

namespace Application.Interfaces;

public interface IPaymentTransactionSerivce
{
    Task<PaymentTransactionResponseDTO> ExecuteTransactionAsync(PaymentTransactionRequestDTO transactionRequestDTO);
}