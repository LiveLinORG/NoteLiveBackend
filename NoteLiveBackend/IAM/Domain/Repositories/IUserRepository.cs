using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.IAM.Domain.Repositories;

/**
 * <summary>
 *  Repository interface for managing user data
 * </summary>
 */
public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    bool ExistsByUsername(string username);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
}