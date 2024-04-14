using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace WebApi.Authorization
{
    public class CheckIfHashValidHandler : AuthorizationHandler<CheckIfHashValidRequirment>
    {
        private readonly string _secretKey;

    public CheckIfHashValidHandler(IConfiguration configuration)
    {
        _secretKey = configuration["SecuritySettings:SecretKey"];
    }

   
   protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckIfHashValidRequirment requirement)
{
    var httpContext = context.Resource as HttpContext;
    if (httpContext == null)
    {
        context.Fail();
        return;
    }

    if (httpContext.Request.Method.ToUpperInvariant() != "POST")
    {
        context.Succeed(requirement);
        return;
    }

    try
    {
        httpContext.Request.EnableBuffering();
        using var reader = new StreamReader(httpContext.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync();
        httpContext.Request.Body.Position = 0;

        var dto = JsonSerializer.Deserialize<PaymentTransactionRequestDTO>(requestBody);
        if (dto == null)
        {
            throw new ArgumentException("Request data is invalid or empty.");
        }

        var payload = $"{dto.TransactionId}|{dto.UserId}|{dto.Currency}|{dto.Amount}|{_secretKey}";
        var headerHash = httpContext.Request.Headers["Hash"].FirstOrDefault();

        if (IsValidHash(payload, headerHash, _secretKey))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
    catch (Exception ex)
    {
        Log.Error($"An error occurred during hash validation: {ex.Message}");
        context.Fail();
    }
}


    private bool IsValidHash(string payload, string headerHash, string secretKey)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
        {
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            string computedHash = Convert.ToBase64String(hashBytes);
            return computedHash == headerHash;
        }
    }
    }
}