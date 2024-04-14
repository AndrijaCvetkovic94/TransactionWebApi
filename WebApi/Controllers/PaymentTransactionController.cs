using Application.DTOs;
using Application.Interfaces;
using Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public PaymentTransactionController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [HttpPost]
        [Authorize(Policy = "ValidHash")]
        public async Task<IActionResult> ExectureTransaction([FromBody] PaymentTransactionRequestDTO transactionDTO, CancellationToken cancellationToken)
        {
            try
            {
                // Map the PaymentTransactionRequestDTO to the actual ExecutePaymentTransactionRequest object required by MediatR.
                var request = new ExecutePaymentTransactionRequest
                {
                    TransactionId = transactionDTO.TransactionId,
                    Amount = transactionDTO.Amount,
                    Currency = transactionDTO.Currency,
                    UserId = transactionDTO.UserId
                };

                // Send the request to the MediatR pipeline and wait for the result.
                var result = await _mediatR.Send(request, cancellationToken);
                
                // Return a successful response if a transaction ID was assigned, otherwise return an error.
                if (result.TransactionId.HasValue)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}