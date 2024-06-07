using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Model.Queries;
using NoteLiveBackend.Users.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Services;

namespace NoteLiveBackend.Users.Aplication.Internal.QueryServices;

public class ProfesorQueryService(IProfesorRepository profesorRepository) : IProfesorQueryService
{
    public async Task<Profesor> Handle(GetProfesorByCodigoProfesorQuery query)
    {
        return await profesorRepository.FindByCodigoProfesorAsync(query.CodigoProfesor);
    }

    public async Task<IEnumerable<Profesor>> Handle(GetProfesorByNameQuery query)
    {
        return await profesorRepository.FindByNameProfesorAsync(query.Name);
    }

    public async Task<Profesor> Handle(GetProfesorByNameAndCodigoProfesorQuery query)
    {
        return await profesorRepository.FindByNameAndProfesorCodigoAsync(query.Name, query.CodigoProfesor);
    }
    
}