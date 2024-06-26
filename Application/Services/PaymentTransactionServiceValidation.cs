using Domain.Interfaces;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{

    // This class provides validation for payment transaction requests.
    public class PaymentTransactionServiceValidation : IPaymentTransactionServiceValidation
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public PaymentTransactionServiceValidation(IUserRepository userRepository, ICurrencyRepository currencyRepository)
        {
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
        }

        // Validates the given payment transaction request and returns a boolean indicating the validity of the request.
        // It also outputs a PaymentTransactionResponseDTO that includes details on the transaction status and any error descriptions.
        public bool ValidateRequest(User user, Currency currency, decimal amount, out PaymentTransactionResponseDTO response)
        {
            if (user == null)
            {
                response = new PaymentTransactionResponseDTO { TransactionId = default, Status = 1, Description = "Non-existing player" };
                return false;
            }

            if (currency == null)
            {
                response = new PaymentTransactionResponseDTO { TransactionId = default, Status = 2, Description = "Invalid Currency" };
                return false;
            }

            if (amount <= 0)
            {
                response = new PaymentTransactionResponseDTO { TransactionId = default, Status = 3, Description = "Amount of money in transaction is not bigger than zero" };
                return false;
            }

            response = new PaymentTransactionResponseDTO();
            return true;
        }
    }
}