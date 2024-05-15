using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    // Use case for executing payment transactions. It implements MediatR's IRequestHandler for handling requests and generating responses.
    internal class ExecutePaymentTransactionUseCase : 
            IRequestHandler<ExecutePaymentTransactionRequest, ExecutePaymentTransactionResponse>
    {
        private readonly IPaymentTransactionServiceValidation _validationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        private readonly ILogger<ExecutePaymentTransactionUseCase> _iLogger;

        public ExecutePaymentTransactionUseCase(IPaymentTransactionServiceValidation validationService, 
            IUserRepository userRepository, 
            ICurrencyRepository currencyRepository,
            IPaymentTransactionRepository paymentTransactionRepository,
            IUnitOfWork unitOfWork,
            ILogger<ExecutePaymentTransactionUseCase> iLogger)
        {
            _validationService = validationService;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _paymentTransactionRepository = paymentTransactionRepository;
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        // Handles the incoming request, checks validation, and processes the payment transaction.
        public async Task<ExecutePaymentTransactionResponse> Handle(ExecutePaymentTransactionRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId, cancellationToken);
            var currency = await _currencyRepository.GetCurrencyAsync(request.Currency, cancellationToken);

            //here we are using PaymentTransactionServiceValidation to validate request
            var isValid = _validationService.ValidateRequest(user, currency, request.Amount, out var result);

            //If request is invalid it will return ExecutePaymentTransactionResponse with null for TransactionId, status diffrent then 0
            //which indicates that transaction was not executed successfully and Description message
            if(!isValid)
            {
                return new ExecutePaymentTransactionResponse
                {
                    Status = result.Status,
                    Description = result.Description,
                    TransactionId = result.TransactionId
                };
            }

            try
            {
                var transaction = await CreateTransacrion(user, currency, request.Amount, cancellationToken);

                return new ExecutePaymentTransactionResponse
                {
                    TransactionId = transaction.Id,
                    Status = (int)StatusOfTransaction.Success,
                    Description = $"Tranaction executed succesfully, {transaction.Amount} of {transaction.TransactionCurrency.ToString()} are transfered to Player {user.Name} {user.LastName} with Id {user.Id}"
                };
            }
            catch (Exception ex)
            {
                return new ExecutePaymentTransactionResponse
                {
                    Status = (int)StatusOfTransaction.InternalFailure,
                    Description = "Transaction failed due to an internal error."
                };
            }
        }

        //Method to create and save a transaction to the database.
        private async Task<PaymentTransaction> CreateTransacrion(User user, Currency currency, decimal amount, CancellationToken cancellationToken)
        {
            
            var transaction = new PaymentTransaction(Guid.NewGuid(), DateTime.UtcNow, amount, currency, user, $"{amount} is transfered to players bank account");
            
            try
            {
                // Add the new transaction to the database.
                await _paymentTransactionRepository.AddTransactionAsync(transaction, cancellationToken);
                
                // Add money to users balance
                user.AddMoneyToUsersAccount(amount);
                
                // Commit the database transaction.
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception("Couldnt add new payment transaction to db ", ex);
            }
        }

        // Helper method to update the user's balance.
        /*private void AddAmountOfMoneyToUsersBalance(User user,decimal amount)
        {
            user.AddMoneyToUsersAccount(amount);
        }*/
    }

    // Data Transfer Object for initiating a payment transaction request.
    public class ExecutePaymentTransactionRequest : IRequest<ExecutePaymentTransactionResponse>
    {
        public Guid TransactionId { get; init; }
        public int UserId { get; init; }
        public string Currency { get; init; }
        public decimal Amount { get; init; }
    }

    // Data Transfer Object for the response of a payment transaction execution.
    public class ExecutePaymentTransactionResponse
    {
        public Guid? TransactionId { get; init; }
        public string Description { get; init; }
        public int Status { get; init; }
    }
}
