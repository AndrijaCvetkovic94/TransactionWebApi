using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    internal class InsufficientFundsException : DomainException
    {
        public InsufficientFundsException() : base("Amount of money is bigger than amount of money in the user's balance.", 4) { }
    }
}
