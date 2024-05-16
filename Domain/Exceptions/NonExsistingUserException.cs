using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    internal class NonExsistingUserException : DomainException
    {
        public NonExsistingUserException() : base("Non-existing player.", 1) { }
    }
}
