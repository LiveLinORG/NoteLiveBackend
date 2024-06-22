using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteLiveBackend.IAM.Domain.Model.Queries;
using NoteLiveBackend.IAM.Interfaces.Resources;
using NoteLiveBackend.IAM.Interfaces.Transform;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController(IUserQueryServices userQueryServices, IUserCommandService userCommandServices) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryServices.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryServices.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetUserByQuery([FromQuery] string name, [FromQuery] string correo, [FromQuery] long? codigoProfesor)
    {
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(correo))
        {
            var query = new GetUserByNameAndCorreoQuery(name, correo);
            var user = await userQueryServices.Handle(query);
            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
            return Ok(userResource);
        }
        else if (!string.IsNullOrEmpty(name))
        {
            var query = new GetUserByNameQuery(name);
            var users = await userQueryServices.Handle(query);
            var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(userResources);
        }
        else if (codigoProfesor.HasValue)
        {
            var query = new GetUserByCodigoProfesorQuery(codigoProfesor.Value);
            var user = await userQueryServices.Handle(query);
            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
            return Ok(userResource);
        }
        else
        {
            return BadRequest("No valid query parameters provided.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserResource resource)
    {
        var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var user = await userCommandServices.Handle(command);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userResource);
    }
}
