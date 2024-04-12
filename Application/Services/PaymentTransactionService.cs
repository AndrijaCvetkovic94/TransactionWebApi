using Domain.Interfaces;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;

namespace Application.Services;

public class PaymentTransactionService : IPaymentTransactionSerivce
{
    private readonly IPaymentTransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;

    public PaymentTransactionService(IPaymentTransactionRepository transactionRepository, IUserRepository userRepository, ICurrencyRepository currencyRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
    }
    
    public async Task<PaymentTransactionResponseDTO> ExecuteTransactionAsync(PaymentTransactionRequestDTO paymentTransactionRequestDTO)
    {
        var user = await _userRepository.GetUserById(paymentTransactionRequestDTO.UserId);

        if(user == null)
        {
            throw new Exception("Non exsisting player");
        }

        Currency currency = await _currencyRepository.GetCurrencyAsync(paymentTransactionRequestDTO.Currency);
        if(currency == null)
        {
            throw new Exception("Invalid currency");    
        }

        if(paymentTransactionRequestDTO.Amount <= 0)
        {
            throw new Exception("Amount value must be bigger than 0 in order to procced with payment transaction");
        }

        var transaction = new PaymentTransaction
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow,
            Amount = paymentTransactionRequestDTO.Amount,
            TransactionCurrency = currency,
            TransactionUser = user,
            Description = $"{paymentTransactionRequestDTO.Amount} is transfered to players bank account"
        };

        await _transactionRepository.AddTransactionAsync(transaction);

        return new PaymentTransactionResponseDTO
        {
            TransactionId = transaction.Id,
            Status = 0,
            Description = "Tranaction executed succesfully"
        };

    }
}