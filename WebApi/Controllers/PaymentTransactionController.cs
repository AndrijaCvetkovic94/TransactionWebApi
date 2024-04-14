using Application.DTOs;
using Application.Interfaces;
using Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
    [Authorize(Policy = "ValidHash")]
    public async Task<IActionResult> ExectureTransaction([FromBody] PaymentTransactionRequestDTO transactionDTO, CancellationToken cancellationToken)
    {
        //Log.Information($"Payment transaction request: Recived request to transfer {transactionDTO.Amount} of {transactionDTO.Currency.ToString()} to players , with Id {transactionDTO.UserId}, account with id {transactionDTO.TransactionId}");

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
            
            //Log.Information($"Payment transaction response: Transaction executed with status: {result.Status} with Id: {result.TransactionId} with Description message: {result.Description}");

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

    [HttpGet]
    public IActionResult GetResponse()
    {
        return Ok("A");
    }
    
}