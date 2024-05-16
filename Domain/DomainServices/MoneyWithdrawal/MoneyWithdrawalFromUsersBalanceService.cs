using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainServices
{
    public class MoneyWithdrawalFromUsersBalanceService : IMoneyWithdrawalFromUsersBalanceService
    {

        public MoneyWithdrawalFromUsersBalanceService()
        {

        }

        public MoneyWithdrawalFromUsersBalance ExecuteMoneyWithdrawal(decimal amount, Currency currency, User user)
        {
            if(user == null)
            {
                throw new NonExsistingUserException();
            }

            if(currency == null)
            {
                throw new InvalidCurrencyException();
            }

            user.ValidateMoneyWithdrawal(amount);

            user.WithdrawMoneyFromBalance(amount);

            return new MoneyWithdrawalFromUsersBalance(Guid.NewGuid(), DateTime.UtcNow, amount, currency, user, $"{amount} of money withdrewed from users balance");
        }
    }
}
