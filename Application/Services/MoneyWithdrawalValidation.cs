using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MoneyWithdrawalValidation : IMoneyWithdrawalValidation
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public MoneyWithdrawalValidation(IUserRepository userRepository, ICurrencyRepository currencyRepository)
        {
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
        }

        public MoneyWithdrawalResponseDTO ValidateRequest(User user, decimal amount, Currency currency)
        {

            if (user == null)
            {
                return new MoneyWithdrawalResponseDTO
                {
                    Status = 1,
                    Description = "Non-existing player"
                };
            }

            if(amount <= 0) 
            {
                return new MoneyWithdrawalResponseDTO
                {
                    Status = 2,
                    Description = "Amount of money in transaction is not bigger than zero"
                };
            }

            if (currency == null)
            {
                return new MoneyWithdrawalResponseDTO
                {
                    Status = 3,
                    Description = "Invalid currency"
                };
            }

            if (user.Balance - amount < 0) 
            {
                return new MoneyWithdrawalResponseDTO
                {
                    Status = 4,
                    Description = "Amount of money is bigger then amount of money in the users balance"
                };
            }

            return new MoneyWithdrawalResponseDTO
            {
                Status = 0,
                Description = $"{amount} of money removed from users balance"
            };
        }
    }
}
