using NoteLiveBackend.Shared.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Model.Aggregates;

namespace NoteLiveBackend.Users.Domain.Repositories;

public interface IProfesorRepository : IBaseRepository<Profesor>
{
    Task<IEnumerable<Profesor>> FindByNameProfesorAsync(string Name);
    Task<Profesor> FindByCodigoProfesorAsync(long CodigoProfesor);
    Task<Profesor?> FindByNameAndProfesorCodigoAsync(string Name, long CodigoProfesor);
}