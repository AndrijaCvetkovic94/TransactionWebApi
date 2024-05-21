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
    internal class ExecuteMoneyWithdrawalUseCase : 
        IRequestHandler<ExecuteMoneyWithdrawalRequest, ExecuteMoneyWithdrawalResponse>
    {
        private readonly IMoneyWithdrawalService _moneyWithdrawalDomainService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMoneyWithdrawalRepository _removeMoneyFromUsersBalanceRepository;

        public ExecuteMoneyWithdrawalUseCase(IMoneyWithdrawalService validationService, 
            IUnitOfWork unitOfWork,
             ICurrencyRepository currencyRepository,
            IUserRepository userRepository,
            IMoneyWithdrawalRepository removeMoneyFromUsersBalanceRepository)
        {
            _moneyWithdrawalDomainService = validationService;
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _removeMoneyFromUsersBalanceRepository = removeMoneyFromUsersBalanceRepository;
        }

        public async Task<ExecuteMoneyWithdrawalResponse> Handle(ExecuteMoneyWithdrawalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserById(request.UserId, cancellationToken);
                var currency = await _currencyRepository.GetCurrencyAsync(request.Currency, cancellationToken);

                var result = _moneyWithdrawalDomainService.ExecuteMoneyWithdrawal(request.Amount, currency, user);

                if(result.IsSuccess)
                {
                    await SaveNewMoneyWithdrawalFromUsersBalance(result.Value, cancellationToken);

                    return new ExecuteMoneyWithdrawalResponse { Status = 0, Description = result.Value.Description };
                }
                else
                {
                    return new ExecuteMoneyWithdrawalResponse { Status = result.Error.Code, Description = result.Error.Message };
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Server Internal error. Unable to execute money withrawal " + ex);
            }
        }

        public async Task SaveNewMoneyWithdrawalFromUsersBalance(MoneyWithdrawal moneyWithdrawal,CancellationToken cancellationToken)
        {
            try
            {
                await _removeMoneyFromUsersBalanceRepository.AddMoneyWithdrawalAsync(moneyWithdrawal, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex ) 
            {
                throw new Exception("Couldnt add new payment money withdrawal to db ", ex);
            }
        }
    }

    public class ExecuteMoneyWithdrawalRequest : IRequest<ExecuteMoneyWithdrawalResponse>
    {
        public int UserId { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; }
    }

    public class ExecuteMoneyWithdrawalResponse
    {
        public string Description { get; init; }
        public int Status { get; init; }
    }
}
