using Application.Interfaces;
using Application.Services;
using Infrastucture.ApplicationServices;
using WebApi.Middlewares.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(Application.AssemblyReference).Assembly));
builder.Services.AddApplicationServices(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IPaymentTransactionServiceValidation, PaymentTransactionServiceValidation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HashValidationMiddleware>(builder.Configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
