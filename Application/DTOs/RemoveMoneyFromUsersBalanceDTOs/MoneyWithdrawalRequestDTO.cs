namespace Application.DTOs;

public class MoneyWithdrawalRequestDTO
{
    public int UserId { get; init; }
    public string Currency { get; init; }
    public decimal Amount { get; init; }
}
