using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    internal class InvalidMoneyAmountException : DomainException
    {
        public InvalidMoneyAmountException() : base("Amount of money in transaction is not bigger than zero.", 2) { }
    }
}
