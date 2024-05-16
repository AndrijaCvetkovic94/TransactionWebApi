using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainServices
{
    public interface IMoneyWithdrawalFromUsersBalanceService
    {
        public MoneyWithdrawalFromUsersBalance ExecuteMoneyWithdrawal(decimal amount, Currency currency, User user);
    }
}
