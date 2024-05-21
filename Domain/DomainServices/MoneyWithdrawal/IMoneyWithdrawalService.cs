using Domain.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainServices
{
    public interface IMoneyWithdrawalService
    {
        public Result<MoneyWithdrawal> ExecuteMoneyWithdrawal(decimal amount, Currency currency, User user);
    }
}
