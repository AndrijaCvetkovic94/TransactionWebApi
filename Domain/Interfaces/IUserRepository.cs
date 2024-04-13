using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserById(int id, CancellationToken cancellationToken);
}