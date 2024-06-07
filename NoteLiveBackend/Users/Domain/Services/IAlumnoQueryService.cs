using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Model.Queries;

namespace NoteLiveBackend.Users.Domain.Services;

public interface IAlumnoQueryService
{
    Task<IEnumerable<Alumno>> Handle(GetAlumnoByNameQuery query);
    Task<Alumno> Handle(GetAlumnoByNameAndCorreoAlumnoQuery query);
    Task<Alumno> Handle(GetAlumnoByCorreoAlumnoQuery query);

    Task<Alumno> Handle(GetAlumnoByIdQuery query);
}