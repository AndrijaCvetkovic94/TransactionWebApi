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
    public class MoneyWithdrawalController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public MoneyWithdrawalController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteMoneyWithdrawalFromUsersBalance([FromBody]MoneyWithdrawalRequestDTO moneyWithdrawalFromUsersBalanceRequestDTO, 
            CancellationToken cancellationToken)
        {
            try
            {
                var request = new ExecuteMoneyWithdrawalRequest {
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
