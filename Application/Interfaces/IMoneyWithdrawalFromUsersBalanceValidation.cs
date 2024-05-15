using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMoneyWithdrawalFromUsersBalanceValidation
    {
        MoneyWithdrawalFromUsersBalanceResponseDTO ValidateRequest(User user, decimal amount, Currency currency);
    }
}