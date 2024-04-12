using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Application.DTOs;

namespace WebApi.Middlewares.Security;

public class HashValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKey;

    public HashValidationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _secretKey = configuration["SecuritySettings:SecretKey"];
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        var bodyStream = new StreamReader(context.Request.Body);
        string requestBody = await bodyStream.ReadToEndAsync();
        context.Request.Body.Position = 0;

        var dto = JsonSerializer.Deserialize<PaymentTransactionRequestDTO>(requestBody);

        var payload = $"{dto.TransactionId}|{dto.UserId}|{dto.Currency}|{dto.Amount}|{_secretKey}";

        string headerHash = context.Request.Headers["Hash"];

        if (!IsValidHash(payload, headerHash))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid hash value.");
            return;
        }

        await _next(context);
    }

    private bool IsValidHash(string payload, string headerHash)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
        {
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            string computedHash = Convert.ToBase64String(hashBytes);
            return computedHash == headerHash;
        }
    }
}