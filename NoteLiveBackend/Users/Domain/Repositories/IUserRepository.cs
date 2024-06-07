using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Users.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<string?> GetUserNameAsync(Guid userId);
    
}

