using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMoneyWithdrawalValidation
    {
        MoneyWithdrawalResponseDTO ValidateRequest(User user, decimal amount, Currency currency);
    }
}