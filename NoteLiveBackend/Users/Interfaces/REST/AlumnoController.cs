using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Users.Domain.Model.Queries;
using NoteLiveBackend.Users.Domain.Services;
using NoteLiveBackend.Users.Interfaces.REST.Resource;
using NoteLiveBackend.Users.Interfaces.REST.Transform;

namespace NoteLiveBackend.Users.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AlumnoController(
    IAlumnoCommandService alumnoCommandService,
    IAlumnoQueryService alumnoQueryService) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult> CreateAlumno([FromBody] CreateAlumnoResource resource)
    {
        var createAlumnCommand =
            CreateAlumnoCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await alumnoCommandService.Handle(createAlumnCommand);
        return CreatedAtAction(nameof(GetAlumnById), new { id = result.Id },
            AlumnoResourceFromEntityAssembler.toResourceFromEntity(result));
    }

    private async Task<ActionResult> GetAlumnoByCodigoAlumno(long codigoAlumno)
    {
        var getAlumnoByCodigoAlumnoQuery = new GetAlumnoByCodigoAlumnoQuery(codigoAlumno);
        var result = await alumnoQueryService.Handle(getAlumnoByCodigoAlumnoQuery);

        var resource = AlumnoResourceFromEntityAssembler.toResourceFromEntity(result);
        return Ok(resource);
    }

    private async Task<ActionResult> GetAlumnoByNameAndCodigoAlumno(string name, long codigoAlumno)
    {
        var getAlumnoByNameAndCodigoAlumnoQuery = new GetAlumnoByNameAndCodigoAlumnoQuery(name, codigoAlumno);
        var result = await alumnoQueryService.Handle(getAlumnoByNameAndCodigoAlumnoQuery);
        var resource = AlumnoResourceFromEntityAssembler.toResourceFromEntity(result);
        return Ok(resource);
    }
    private async Task<ActionResult> GetAlumnoByName(string name)
    {
        var getAlumnoByNameQuery = new GetAlumnoByNameQuery(name);
        var result = await alumnoQueryService.Handle(getAlumnoByNameQuery);
        var resource = result.Select(AlumnoResourceFromEntityAssembler.toResourceFromEntity);
        return Ok(resource);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAlumnById(int id)
    {
        var getAlumnoByIdQuery = new GetAlumnoByIdQuery(id);
        var result = await alumnoQueryService.Handle(getAlumnoByIdQuery);
        if (result is null) return NotFound();
        var resource = AlumnoResourceFromEntityAssembler.toResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAlumnoFromQuery(
        [FromQuery] string name,
        [FromQuery] long codigoAlumno,
        [FromQuery] string email = "")
    {
        if (!string.IsNullOrEmpty(name) && codigoAlumno > 0)
        {
            return await GetAlumnoByNameAndCodigoAlumno(name, codigoAlumno);
        }
        else if (!string.IsNullOrEmpty(name))
        {
            return await GetAlumnoByName(name);
        }
        else
        {
            return await GetAlumnoByCodigoAlumno(codigoAlumno);
        }
    }
    
    
}