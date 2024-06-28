using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Model.Queries;
using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.IAM.Domain.Services;

namespace NoteLiveBackend.IAM.Application.Internal.QueryServices;

/**
 * <summary>
 *  This class is responsible for handling user queries
 * </summary>
 */
public class UserQueryService : IUserQueryServices
{
    private readonly IUserRepository _userRepository;
    
    /**
     * <summary>
     *  Initializes a new instance
     * </summary>
     */
    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    /**
     * <summary>
     *  Gets a user by Id 
     * </summary>
     * <param name="query">The query containing the user id</param>
     * <returns>The user</returns>
     */
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await _userRepository.FindByIdAsync(query.Id);
    }
    
    /**
     * <summary>
     *  Gets a user by username
     * </summary>
     * <param name="query">The query containing the username</param>
     * <returns>The user</returns>
     */
    public async Task<User?> Handle(GetUserByNameQuery query)
    
    {
        return await _userRepository.FindByUsernameAsync(query.Name);

    }

    /**
     * <summary>
     *  Gets all users
     * </summary>
     * <param name="query">The query</param>
     * <returns>All the users</returns>
     */
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await _userRepository.GetAllAsync();
    }



}