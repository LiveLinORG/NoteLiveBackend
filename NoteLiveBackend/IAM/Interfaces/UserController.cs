using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.IAM.Domain.Model.Queries;
using NoteLiveBackend.IAM.Domain.Services;
using NoteLiveBackend.IAM.Interfaces.Transform;

namespace NoteLiveBackend.IAM.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController(IUserQueryServices userQueryServices) : ControllerBase
{
    /**
     * <summary>
     *  Get all the users
     * </summary>
     * <returns>All the users</returns>
     */
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryServices.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
    
    /**
     * <summary>
     *  Get a user by Id
     * </summary>
     * <param name="id">The id of the user to get</param>
     * <returns>The user</returns>
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryServices.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }
    
    /**
     * <summary>
     *  Get a user by username
     * </summary>
     * <param name="username">The username of the user to get</param>
     * <returns>The user</returns>
     */
    [HttpGet("getbyusername/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var getUserByIdQuery = new GetUserByNameQuery(username);
        var user = await userQueryServices.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }
}