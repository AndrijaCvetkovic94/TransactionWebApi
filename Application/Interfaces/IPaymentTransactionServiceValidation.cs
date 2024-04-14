using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPaymentTransactionServiceValidation
{
    bool ValidateRequest(User user, Currency currency, decimal amount, out PaymentTransactionResponseDTO response);
}