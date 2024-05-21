using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Exceptions;

namespace Domain.Entities;

public class User
{
    public int Id { get; init; }

    public string Name { get; private set; } = string.Empty;
    
    public string LastName { get; private set; } = string.Empty;

    public string AccountNumber { get; private set; } = string.Empty;

    public string Adress { get; private set; } = string.Empty;
    
    public decimal Balance { get; private set; }

    public List<PaymentTransaction> Transactions { get; private set; } = new List<PaymentTransaction>();

    public List<MoneyWithdrawal> MoneyWithdrawals { get; private set; } = new List<MoneyWithdrawal>();

    public void AddMoneyToUsersAccount(decimal amount)
    {
        Balance += amount;
    }

    internal void WithdrawMoneyFromBalance(decimal amount)
    {
        Balance -= amount;
    }

    internal bool ValidateMoneyWithdrawal(decimal amount)
    {
        if(Balance < amount)
        {
            return false;
        }

        return true;
    }
}