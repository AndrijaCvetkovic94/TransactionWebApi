using Domain.Entities;

namespace Domain.Interfaces;

public interface ICurrencyRepository
{
    Task<Currency> GetCurrencyAsync(string CurrencyCode); 
}