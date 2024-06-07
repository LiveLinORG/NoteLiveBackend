using NoteLiveBackend.Shared.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Model.Aggregates;

namespace NoteLiveBackend.Users.Domain.Repositories;

public interface IAlumnoRepository : IBaseRepository<Alumno>
{
    Task<IEnumerable<Alumno>> FindByNameAlumnoAsync(string Name);
    Task<Alumno?> FindByCorreoAlumnoAsync(string correoAlumno);
    Task<Alumno?> FindByNameAndCorreoAlumnoAsync(string Name, string correoAlumno);
}