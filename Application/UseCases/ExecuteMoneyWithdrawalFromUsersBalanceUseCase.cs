using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    internal class ExecuteMoneyWithdrawalFromUsersBalanceUseCase : 
        IRequestHandler<ExecuteRemoveMoneyFromUsersBalanceRequest, ExecuteRemoveMoneyFromUsersBalanceResponse>
    {

        private readonly IMoneyWithdrawalFromUsersBalanceValidation _validationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMoneyWithdrawalFromUsersBalanceRepository _removeMoneyFromUsersBalanceRepository;

        public ExecuteMoneyWithdrawalFromUsersBalanceUseCase(IMoneyWithdrawalFromUsersBalanceValidation validationService, 
            IUnitOfWork unitOfWork,
             ICurrencyRepository currencyRepository,
            IUserRepository userRepository,
            IMoneyWithdrawalFromUsersBalanceRepository removeMoneyFromUsersBalanceRepository)
        {
            _validationService = validationService;
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _removeMoneyFromUsersBalanceRepository = removeMoneyFromUsersBalanceRepository;
        }

        public async Task<ExecuteRemoveMoneyFromUsersBalanceResponse> Handle(ExecuteRemoveMoneyFromUsersBalanceRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId, cancellationToken);
            var currency = await _currencyRepository.GetCurrencyAsync(request.Currency, cancellationToken);

            var response = _validationService.ValidateRequest(user, request.Amount, currency);

            if(response.Status == 0)
            {
            }

            return new ExecuteRemoveMoneyFromUsersBalanceResponse { Status = response.Status, Description = response.Description};

        }

        public async Task<MoneyWithdrawalFromUsersBalance> CreateMoneyWithdrawalFromUsersBalance(User user, Currency currency, decimal amount, CancellationToken cancellationToken)
        {
            var moneyWithdrawal = new MoneyWithdrawalFromUsersBalance(Guid.NewGuid(), DateTime.UtcNow, amount, currency, user, $"{amount} is withdrawed from players balance");

            try
            {
                await _removeMoneyFromUsersBalanceRepository.AddMoneyWithdrawalAsync(moneyWithdrawal, cancellationToken);
                
                user.RemoveMoneyFromUsersAccount(amount);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return moneyWithdrawal;
            }
            catch(Exception ex ) 
            {
                throw new Exception("Couldnt add new payment money withdrawal to db ", ex);
            }
        }
    }

    public class ExecuteRemoveMoneyFromUsersBalanceRequest : IRequest<ExecuteRemoveMoneyFromUsersBalanceResponse>
    {
        public int UserId { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; }
    }

    public class ExecuteRemoveMoneyFromUsersBalanceResponse
    {
        public string Description { get; init; }
        public int Status { get; init; }
    }
}
