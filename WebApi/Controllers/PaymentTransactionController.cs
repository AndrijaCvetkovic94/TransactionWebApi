using Application.DTOs;
using Application.Interfaces;
using Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

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
    public async Task<IActionResult> ExectureTransaction([FromBody] PaymentTransactionRequestDTO transactionDTO, CancellationToken cancellationToken)
    {
        try
        {
            var request = new ExecutePaymentTransactionRequest
            {
                TransactionId = transactionDTO.TransactionId,
                Amount = transactionDTO.Amount,
                Currency = transactionDTO.Currency,
                UserId = transactionDTO.UserId
            };

            var result = await _mediatR.Send(request, cancellationToken);

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