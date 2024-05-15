namespace Application.DTOs;

public class MoneyWithdrawalFromUsersBalanceRequestDTO
{
    public int UserId { get; init; }
    public string Currency { get; init; }
    public decimal Amount { get; init; }
}
