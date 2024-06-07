using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Model.Queries;
using NoteLiveBackend.Users.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Services;

namespace NoteLiveBackend.Users.Aplication.Internal.QueryServices;

public class AlumnoQueryService(IAlumnoRepository alumnoRepository) : IAlumnoQueryService
{
    public async Task<Alumno> Handle(GetAlumnoByCorreoAlumnoQuery query)
    {
        return await alumnoRepository.FindByCorreoAlumnoAsync(query.correoAlumno);
    }

    public async Task<IEnumerable<Alumno>> Handle(GetAlumnoByNameQuery query)
    {
        return await alumnoRepository.FindByNameAlumnoAsync(query.Name);
    }

    public async Task<Alumno> Handle(GetAlumnoByNameAndCorreoAlumnoQuery query)
    {
        return await alumnoRepository.FindByNameAndCorreoAlumnoAsync(query.Name, query.correoAlumno);
    }

    public async Task<Alumno> Handle(GetAlumnoByIdQuery query)
    {
        return await alumnoRepository.FindByIdAsync(query.Id);
    }
    
}