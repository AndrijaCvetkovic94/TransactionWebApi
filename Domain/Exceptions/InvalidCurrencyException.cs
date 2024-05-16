using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    internal class InvalidCurrencyException : DomainException
    {
        public InvalidCurrencyException() : base("Invalid currency.", 3) { }
    }
}
