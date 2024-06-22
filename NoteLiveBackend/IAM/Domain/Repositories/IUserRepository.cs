

using NoteLiveBackend.Shared.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);

    bool ExistsByUsername(string username);
}