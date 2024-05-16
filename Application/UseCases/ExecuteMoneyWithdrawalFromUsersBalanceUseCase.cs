using Application.Interfaces;
using Domain.DomainServices;
using Domain.Exceptions;
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
        private readonly IMoneyWithdrawalFromUsersBalanceService _moneyWithdrawalDomainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMoneyWithdrawalFromUsersBalanceRepository _removeMoneyFromUsersBalanceRepository;

        public ExecuteMoneyWithdrawalFromUsersBalanceUseCase(IMoneyWithdrawalFromUsersBalanceService validationService, 
            IUnitOfWork unitOfWork,
             ICurrencyRepository currencyRepository,
            IUserRepository userRepository,
            IMoneyWithdrawalFromUsersBalanceRepository removeMoneyFromUsersBalanceRepository)
        {
            _moneyWithdrawalDomainService = validationService;
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _removeMoneyFromUsersBalanceRepository = removeMoneyFromUsersBalanceRepository;
        }

        public async Task<ExecuteRemoveMoneyFromUsersBalanceResponse> Handle(ExecuteRemoveMoneyFromUsersBalanceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserById(request.UserId, cancellationToken);
                var currency = await _currencyRepository.GetCurrencyAsync(request.Currency, cancellationToken);

                var moneyWithdrawalFromUsersBalance = _moneyWithdrawalDomainService.ExecuteMoneyWithdrawal(request.Amount, currency, user);
                await SaveNewMoneyWithdrawalFromUsersBalance(moneyWithdrawalFromUsersBalance,cancellationToken);

                return new ExecuteRemoveMoneyFromUsersBalanceResponse { Status = 0, Description = moneyWithdrawalFromUsersBalance.Description};
            }
            catch(DomainException ex)
            {
                return new ExecuteRemoveMoneyFromUsersBalanceResponse { Status = ex.StatusCode, Description = ex.Message};
            }
        }

        public async Task SaveNewMoneyWithdrawalFromUsersBalance(MoneyWithdrawalFromUsersBalance moneyWithdrawalFromUsersBalance,CancellationToken cancellationToken)
        {
            try
            {
                await _removeMoneyFromUsersBalanceRepository.AddMoneyWithdrawalAsync(moneyWithdrawalFromUsersBalance, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
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
