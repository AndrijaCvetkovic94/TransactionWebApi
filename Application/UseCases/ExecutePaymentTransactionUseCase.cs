using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
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

        public async Task<ExecutePaymentTransactionResponse> Handle(ExecutePaymentTransactionRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId, cancellationToken);
            var currency = await _currencyRepository.GetCurrencyAsync(request.Currency, cancellationToken);

            var isValid = _validationService.ValidateRequest(user, currency, request.Amount, out var result);

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
                    Description = $"Tranaction executed succesfully, {transaction.Amount} of {transaction.TransactionCurrency} are transfered to Player {user.Name} {user.LastName} with Id {user.Id}"
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

        private async Task<PaymentTransaction> CreateTransacrion(User user, Currency currency, decimal amount, CancellationToken cancellationToken)
        {
            var transaction = new PaymentTransaction
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                Amount = amount,
                TransactionCurrency = currency,
                TransactionUser = user,
                Description = $"{amount} is transfered to players bank account"
            };
            
            try
            {

                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                await _paymentTransactionRepository.AddTransactionAsync(transaction, cancellationToken);
                
                await _unitOfWork.CommitAsync(cancellationToken);
                
                await _unitOfWork.CommitTransactionAsync(cancellationToken);


                return transaction;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw new Exception("Couldnt add new payment transaction to db ", ex);
            }
        }
    }

    public class ExecutePaymentTransactionRequest : IRequest<ExecutePaymentTransactionResponse>
    {
        public Guid TransactionId { get; init; }
        public int UserId { get; init; }
        public string Currency { get; init; }
        public decimal Amount { get; init; }
    }

    public class ExecutePaymentTransactionResponse
    {
        public Guid? TransactionId { get; init; }
        public string Description { get; init; }
        public int Status { get; init; }
    }
}
