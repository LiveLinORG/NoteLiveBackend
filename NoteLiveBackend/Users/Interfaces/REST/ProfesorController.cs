using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Users.Domain.Model.Commands;
using NoteLiveBackend.Users.Domain.Model.Queries;
using NoteLiveBackend.Users.Domain.Services;
using NoteLiveBackend.Users.Interfaces.REST.Resource;
using NoteLiveBackend.Users.Interfaces.REST.Transform;

namespace NoteLiveBackend.Users.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class ProfesorController(
    IProfesorCommandService profesorCommandService,
    IProfesorQueryService profesorQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateProfesor([FromBody] CreateProfesorResource resource)
    {
        var createProfesorCommand =
            CreateProfesorCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await profesorCommandService.Handle(createProfesorCommand);
        return CreatedAtAction(nameof(GetProfesorById), new { id = result.Id },
            ProfesorResourceFromEntityAssembler.toResourceFromEntity(result));
    }
    
    private async Task<ActionResult> GetProfesorByCodigoProfesor(long codigoProfesor)
    {
        var getProfesorByCodigoProfesorQuery = new GetProfesorByCodigoProfesorQuery(codigoProfesor);
        var result = await profesorQueryService.Handle(getProfesorByCodigoProfesorQuery);

        var resource = ProfesorResourceFromEntityAssembler.toResourceFromEntity(result);
        return Ok(resource);
    }
    
    private async Task<ActionResult> GetProfesorByNameAndCodigoProfesor(string name, long codigoProfesor)
    {
        var getProfesorByNameAndCodigoProfesorQuery = new GetProfesorByNameAndCodigoProfesorQuery(name, codigoProfesor);
        var result = await profesorQueryService.Handle(getProfesorByNameAndCodigoProfesorQuery);
        var resource = ProfesorResourceFromEntityAssembler.toResourceFromEntity(result);
        return Ok(resource);
    }
    private async Task<ActionResult> GetProfesorByName(string name)
    {
        var getProfesorByNameQuery = new GetProfesorByNameQuery(name);
        var result = await profesorQueryService.Handle(getProfesorByNameQuery);
        var resource = result.Select(ProfesorResourceFromEntityAssembler.toResourceFromEntity);
        return Ok(resource);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetProfesorById(Guid id)
    {
        var getProfesorByIdQuery = new GetProfesorByIdQuery(id);
        var result = await profesorQueryService.Handle(getProfesorByIdQuery);
        if (result is null) return NotFound();
        var resource = ProfesorResourceFromEntityAssembler.toResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetProfesorFromQuery(
        [FromQuery] string name,
        [FromQuery] long? codigoProfesor,
        [FromQuery] string email = "")
    {
        if (!string.IsNullOrEmpty(name) && codigoProfesor.HasValue && codigoProfesor.Value >= 0)
        {
            return await GetProfesorByNameAndCodigoProfesor(name, codigoProfesor.Value);
        }
        else if (!string.IsNullOrEmpty(name))
        {
            return await GetProfesorByName(name);
        }
       
        else
        {
            return BadRequest("No valid query parameters provided.");
        }
    }
    
}