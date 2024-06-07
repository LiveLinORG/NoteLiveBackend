using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Model.Queries;

namespace NoteLiveBackend.Users.Domain.Services;

public interface IProfesorQueryService
{
    Task<IEnumerable<Profesor>> Handle(GetProfesorByNameQuery query);
    Task<Profesor> Handle(GetProfesorByNameAndCodigoProfesorQuery query);
    Task<Profesor> Handle(GetProfesorByCodigoProfesorQuery query);
}