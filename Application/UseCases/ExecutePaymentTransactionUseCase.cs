using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.UseCases
{
    internal class ExecutePaymentTransactionUseCase : 
            IRequestHandler<ExecutePaymentTransactionRequest, ExecutePaymentTransactionResponse>
    {
        private readonly IPaymentTransactionServiceValidation _validationService;

        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;

        public ExecutePaymentTransactionUseCase(IPaymentTransactionServiceValidation validationService, 
            IUserRepository userRepository, 
            ICurrencyRepository currencyRepository,
            IPaymentTransactionRepository paymentTransactionRepository)
        {
            _validationService = validationService;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _paymentTransactionRepository = paymentTransactionRepository;
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

            var transaction = await CreateTransacrion(user, currency, request.Amount, cancellationToken);

            return new ExecutePaymentTransactionResponse
            {
                TransactionId = transaction.Id,
                Status = (int)StatusOfTransaction.Success,
                Description = $"Tranaction executed succesfully, {transaction.Amount} of {transaction.TransactionCurrency} are transfered to Player {user.Name} {user.LastName} with Id {user.Id}"
            };
        }

        private async Task<PaymentTransaction> CreateTransacrion(User user, Currency currency, int amount, CancellationToken cancellationToken)
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

            await _paymentTransactionRepository.AddTransactionAsync(transaction, cancellationToken);

            return transaction;
        }
    }
    public class ExecutePaymentTransactionRequest : IRequest<ExecutePaymentTransactionResponse>
    {
        public Guid TransactionId { get; init; }
        public int UserId { get; init; }
        public string Currency { get; init; }
        public int Amount { get; init; }
    }

    public class ExecutePaymentTransactionResponse
    {
        public Guid? TransactionId { get; init; }
        public string Description { get; init; }
        public int Status { get; init; }
    }
}
