using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentTransactionController : ControllerBase
{
    private readonly IPaymentTransactionSerivce _paymentTransactionSerivce;

    public PaymentTransactionController(IPaymentTransactionSerivce paymentTransactionSerivce)
    {
        _paymentTransactionSerivce = paymentTransactionSerivce;
    }

    [HttpPost]
    public async Task<IActionResult> ExectureTransaction([FromBody] PaymentTransactionRequestDTO transactionDTO)
    {
        try{
            var result = await _paymentTransactionSerivce.ExecuteTransactionAsync(transactionDTO);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    
}