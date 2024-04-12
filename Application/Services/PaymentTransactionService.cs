using Domain.Interfaces;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;

namespace Application.Services;

public class PaymentTransactionService : IPaymentTransactionSerivce
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;

    public PaymentTransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ICurrencyRepository currencyRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
    }
    
    public Task<PaymentTransactionResponseDTO> ExecuteTransactionAsync(PaymentTransactionRequestDTO transactionRequestDTO)
    {
        throw new NotImplementedException();
    }
}