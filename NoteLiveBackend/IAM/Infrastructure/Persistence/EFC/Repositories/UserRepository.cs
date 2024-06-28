using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace NoteLiveBackend.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }
    
    /**
    * <summary>
    *  The User repository
    * </summary>
    * <remarks>
    *  This repository is used to managed users
    * </remarks>
    */
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }
    
    /**
     * <summary>
     *  Check if a user exists by username
     * </summary>
     * <param name="username">The username to search</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }


    /**
    * <summary>
    * Get all users from the database
    * </summary>
    * <returns>A collection of users</returns>
    */
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Context.Set<User>().ToListAsync();
    }
    
    /**
    * <summary>
    * Get a user by Id
    * </summary>
    * <param name="userId">The unique identifier of the user to retrieve</param>
    * <returns>The user</returns>
    */
    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await Context.Set<User>().FindAsync(userId);
    }
    




}