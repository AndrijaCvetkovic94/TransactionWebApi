using Application.DTOs;
using Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyWithdrawalFromUsersBalanceController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public MoneyWithdrawalFromUsersBalanceController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [HttpPost]
        [Authorize(Policy = "ValidHash")]
        public async Task<IActionResult> ExecuteMoneyWithdrawalFromUsersBalance([FromBody]MoneyWithdrawalFromUsersBalanceRequestDTO moneyWithdrawalFromUsersBalanceRequestDTO, 
            CancellationToken cancellationToken)
        {
            try
            {
                var request = new ExecuteRemoveMoneyFromUsersBalanceRequest {
                    Amount = moneyWithdrawalFromUsersBalanceRequestDTO.Amount,
                    Currency = moneyWithdrawalFromUsersBalanceRequestDTO.Currency,
                    UserId = moneyWithdrawalFromUsersBalanceRequestDTO.UserId
                };

                var result = await _mediatR.Send(request, cancellationToken);

                if (result.Status == 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
