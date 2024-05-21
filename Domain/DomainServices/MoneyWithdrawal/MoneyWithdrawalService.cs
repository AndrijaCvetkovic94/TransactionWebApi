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
                return Result.Failure<MoneyWithdrawal>(new Error(1, "Non exsisting user"));
            }

            if(currency == null)
            {
                return Result.Failure<MoneyWithdrawal>(new Error(2, "Invalid currency"));
            }

            if(amount <= 0)
            {
                return Result.Failure<MoneyWithdrawal>(new Error(3, "Amount must be bigger then 0"));
            }

            if (!user.ValidateMoneyWithdrawal(amount))
            {
                return Result.Failure<MoneyWithdrawal>(new Error(4, "Not enough funds on users balance"));
            }

            user.WithdrawMoneyFromBalance(amount);

            return new MoneyWithdrawal(Guid.NewGuid(), DateTime.UtcNow, amount, currency, user, $"{amount} of money withdrewed from users balance");
        }
    }
}
