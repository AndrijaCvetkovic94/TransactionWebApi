using Domain.Entities;
using Domain.Exceptions;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainServices
{
    public class MoneyWithdrawalService : IMoneyWithdrawalService
    {

        public MoneyWithdrawalService()
        {

        }

        public Result<MoneyWithdrawal> ExecuteMoneyWithdrawal(decimal amount, Currency currency, User user)
        {
            if(user == null)
            {
                return Result.Failure<MoneyWithdrawal>(new Error("1", "Non exsisting user"));
            }

            if(currency == null)
            {
                throw new InvalidCurrencyException();
            }

            user.ValidateMoneyWithdrawal(amount);

            user.WithdrawMoneyFromBalance(amount);

            return new MoneyWithdrawal(Guid.NewGuid(), DateTime.UtcNow, amount, currency, user, $"{amount} of money withdrewed from users balance");
        }
    }
}
