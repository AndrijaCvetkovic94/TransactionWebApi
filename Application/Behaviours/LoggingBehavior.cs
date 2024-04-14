using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    //This class logs request and response.It is part of MediatR pipelin
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        // This method is called when the pipeline is executing.
        // It intercepts the request, logs it, then proceeds with the next delegate in the pipeline and finally logs the response.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {   
            //logging request
            var reqBody = JsonSerializer.Serialize(request);
            _logger.LogInformation($"Handling Request {reqBody}");

            //here we are passing control to next delegate which in our case is ExecutePaymentTransactionUseCase.Handle method
            //which will validate request and if everything that is sent is valid will generate PaymentTransaction and return
            //ExecutePaymentTransactionResponse DTO
            var response = await next();
            
            var resBody = JsonSerializer.Serialize(response);

            //logging response
            _logger.LogInformation($"Response {resBody}");

            return response;
        }
    }
}
