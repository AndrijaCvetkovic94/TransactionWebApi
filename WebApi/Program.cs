using Application.Interfaces;
using Application.Services;
using Infrastucture.ApplicationServices;
using Serilog;
using WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using WebApi.Authorization.ResultHandler;
using Domain.Interfaces;
using Infrastructure.Data;
using Application.Behaviours;
using Domain.DomainServices;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => 
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext());

builder.Services.AddApplicationServices(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ValidHash", policy =>
            policy.Requirements.Add(new CheckIfHashValidRequirment()));
    });


builder.Services.AddSingleton<IAuthorizationHandler, CheckIfHashValidHandler>();

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationResultHandler>();

builder.Services.AddScoped<IPaymentTransactionServiceValidation, PaymentTransactionServiceValidation>();

builder.Services.AddScoped<IMoneyWithdrawalService, MoneyWithdrawalService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(Application.AssemblyReference).Assembly).AddOpenBehavior(typeof(LoggingBehavior<,>)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//This was first solution for authorization, using middleware to check if hash is valid
//app.UseMiddleware<HashValidationMiddleware>(builder.Configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
